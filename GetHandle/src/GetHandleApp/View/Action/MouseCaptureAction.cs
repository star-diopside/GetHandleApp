using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace GetHandleApp.View.Action
{
    class MouseCaptureAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty MouseCaptureProperty =
            DependencyProperty.Register("MouseCapture", typeof(bool), typeof(MouseCaptureAction), new UIPropertyMetadata());
        public static readonly DependencyProperty CursorResourceNameProperty =
            DependencyProperty.Register("CursorResourceName", typeof(string), typeof(MouseCaptureAction), new UIPropertyMetadata());

        public bool MouseCapture
        {
            get { return (bool)GetValue(MouseCaptureProperty); }
            set { SetValue(MouseCaptureProperty, value); }
        }

        public string CursorResourceName
        {
            get { return (string)GetValue(CursorResourceNameProperty); }
            set { SetValue(CursorResourceNameProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (this.MouseCapture)
            {
                this.AssociatedObject.CaptureMouse();

                if (!string.IsNullOrWhiteSpace(this.CursorResourceName))
                {
                    byte[] res = (byte[])Properties.Resources.ResourceManager.GetObject(this.CursorResourceName, Properties.Resources.Culture);
                    Mouse.OverrideCursor = new Cursor(new MemoryStream(res, false));
                }
            }
            else
            {
                this.AssociatedObject.ReleaseMouseCapture();
                Mouse.OverrideCursor = null;
            }
        }
    }
}
