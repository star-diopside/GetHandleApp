using GetHandle.Wpf.Module.Properties;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace GetHandle.Wpf.Module.Views.Action
{
    class MouseCaptureAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty MouseCaptureProperty =
            DependencyProperty.Register(nameof(MouseCapture), typeof(bool), typeof(MouseCaptureAction), new UIPropertyMetadata());
        public static readonly DependencyProperty CursorResourceNameProperty =
            DependencyProperty.Register(nameof(CursorResourceName), typeof(string), typeof(MouseCaptureAction), new UIPropertyMetadata());

        public bool MouseCapture
        {
            get => (bool)GetValue(MouseCaptureProperty);
            set => SetValue(MouseCaptureProperty, value);
        }

        public string CursorResourceName
        {
            get => (string)GetValue(CursorResourceNameProperty);
            set => SetValue(CursorResourceNameProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            if (MouseCapture)
            {
                AssociatedObject.CaptureMouse();

                if (!string.IsNullOrWhiteSpace(CursorResourceName))
                {
                    byte[] res = (byte[])Resources.ResourceManager.GetObject(CursorResourceName, Resources.Culture);
                    Mouse.OverrideCursor = new Cursor(new MemoryStream(res, false));
                }
            }
            else
            {
                AssociatedObject.ReleaseMouseCapture();
                Mouse.OverrideCursor = null;
            }
        }
    }
}
