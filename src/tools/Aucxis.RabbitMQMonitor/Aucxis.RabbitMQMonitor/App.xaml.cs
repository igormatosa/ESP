using Prism.Ioc;
using Aucxis.RabbitMQMonitor.Views;
using System.Windows;
using Prism.Modularity;
using Aucxis.RabbitMQMonitor.InputOutputModule.Dialogs;

namespace Aucxis.RabbitMQMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<MessageDialog, MessageDialogViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<InputOutputModule.InputOutputModule>();
        }
    }
}
