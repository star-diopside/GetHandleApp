using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace GetHandle.Wpf.Module.Views.Action
{
    class ChangeImageAction : TriggerAction<Image>
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(ChangeImageAction), new UIPropertyMetadata());

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            AssociatedObject.Source = ImageSource;
        }
    }
}
