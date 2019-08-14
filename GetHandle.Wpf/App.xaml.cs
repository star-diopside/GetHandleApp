using GetHandle.Wpf.Module;
using GetHandle.Wpf.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using System.Windows.Threading;
using WindowHandleImplement.Function;
using WindowHandleInterface.Function;

namespace GetHandle.Wpf
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IWindowProcFactory, WindowProcFactory>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<GetHandleModule>();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
