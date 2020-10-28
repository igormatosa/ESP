using Aucxis.RabbitMQMonitor.InputOutputModule.Dialogs;
using Aucxis.RabbitMQMonitor.InputOutputModule.ViewModels;
using Aucxis.RabbitMQMonitor.InputOutputModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Aucxis.RabbitMQMonitor.InputOutputModule
{
    public class InputOutputModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public InputOutputModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("InputOutputRegion", typeof(InputOutputView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<InputOutputView, InputOutputViewModel>();
            containerRegistry.RegisterForNavigation<MessageDialog, MessageDialogViewModel>();
        }
    }
}
