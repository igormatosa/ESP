using Prism.Services.Dialogs;
using System;

namespace Aucxis.RabbitMQMonitor.InputOutputModule.Dialogs
{
    public static class IDialogServiceExtensions
    {
        public static void ShowMessageDialog(this IDialogService dialogService, string title, string message, Action<IDialogResult> callback)
        {
            var p = new DialogParameters();
            p.Add("message", message);
            p.Add("title", title);

            dialogService.ShowDialog("MessageDialog", p, callback);
        }
    }
}
