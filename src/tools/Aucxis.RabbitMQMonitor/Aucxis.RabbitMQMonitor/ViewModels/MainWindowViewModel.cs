using Prism.Mvvm;

namespace Aucxis.RabbitMQMonitor.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MQ Tool";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
