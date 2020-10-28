using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Aucxis.RabbitMQMonitor.InputOutputModule.Dialogs
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        //public string Title => "Warning";

        public string Title { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand CloseDialogCommand { get; }

        public MessageDialogViewModel()
        {
            CloseDialogCommand = new DelegateCommand(CloseDialog);
        }

        private void CloseDialog()
        {
            var buttonResult = ButtonResult.OK;
            RequestClose?.Invoke(new DialogResult(buttonResult));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("title");
            Message = parameters.GetValue<string>("message");
        }
    }
}
