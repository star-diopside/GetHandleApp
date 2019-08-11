using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace GetHandleApp.View.Action
{
    class ChangeImageAction : TriggerAction<Image>
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ChangeImageAction), new UIPropertyMetadata());

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            this.AssociatedObject.Source = this.ImageSource;
        }
    }
}
