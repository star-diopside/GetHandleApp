using GetHandle.Wpf.Module.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace GetHandle.Wpf.Module
{
    public class GetHandleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainContent", typeof(MainView));
            regionManager.RegisterViewWithRegion("MainMenu", typeof(MainMenuView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
