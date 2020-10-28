using Aucxis.RabbitMQMonitor.InputOutputModule.Dialogs;
using Aucxis.RabbitMQMonitor.InputOutputModule.Models;
using Prism.Commands;
using Prism.Services.Dialogs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Aucxis.RabbitMQMonitor.InputOutputModule.ViewModels
{
    public class InputOutputViewModel : Prism.Mvvm.BindableBase
    {
        private IModel _channel;
        private readonly IDialogService _dialogService;

        #region Properties
        private string _ipAddress;
        public string IpAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }

        private string _exchange;
        public string Exchange
        {
            get { return _exchange; }
            set { SetProperty(ref _exchange, value); }
        }

        private string _inputRoutingKey;
        public string InputRoutingKey
        {
            get { return _inputRoutingKey; }
            set { SetProperty(ref _inputRoutingKey, value); }
        }

        private string oldOutputRoutingKeys;
        private string _outputRoutingKeys;
        public string OutputRoutingKeys
        {
            get { return _outputRoutingKeys; }
            set { SetProperty(ref _outputRoutingKeys, value); }
        }

        private string _inputMessage = "test";
        public string InputMessage
        {
            get { return _inputMessage; }
            set { SetProperty(ref _inputMessage, value); }
        }

        private string _outputMessage;
        public string OutputMessage
        {
            get { return _outputMessage; }
            set { SetProperty(ref _outputMessage, value); }
        }

        public ObservableCollection<RabbitMQMessage> MessageList { get; set; }

        private RabbitMQMessage _selectedMessage;
        public RabbitMQMessage SelectedMessage
        {
            get { return _selectedMessage; }
            set
            {
                if (value == _selectedMessage) return;

                _selectedMessage = value;

                if(value is null)
                {
                    OutputMessage = string.Empty;
                }
                else
                {
                    OutputMessage = value.Body;
                }
                RaisePropertyChanged(nameof(OutputMessage));
            }
        }

        public bool CommunicationEnabled { get; set; }
        #endregion

        public InputOutputViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            MessageList = new ObservableCollection<RabbitMQMessage>();
            TestConnectionCommand = new DelegateCommand(TestConnection)
                .ObservesCanExecute(() => CanExecuteTestConnection);
            SendMessageCommand = new DelegateCommand(SendMessage)
                .ObservesCanExecute(() => CanExecuteSendMessage);
            ReceiveMessagesCommand = new DelegateCommand(ReceiveMessages)
                .ObservesCanExecute(() => CanExecuteReceiveMessages);
            ClearMessagesCommand = new DelegateCommand(ClearMessages);
            CanExecuteTestConnection = true;
        }

        #region TestConnectionCommand
        public DelegateCommand TestConnectionCommand { get; private set; }

        private bool _canExecuteTestConnection;
        public bool CanExecuteTestConnection
        {
            get { return _canExecuteTestConnection; }
            set { SetProperty(ref _canExecuteTestConnection, value); }
        }

        private void TestConnection()
        {
            CanExecuteTestConnection = false;

            if (string.IsNullOrEmpty(IpAddress))
            {
                _dialogService.ShowMessageDialog("Warning", "IP address is empty!", null);
                CanExecuteTestConnection = true;
                return;
            }
            if (string.IsNullOrEmpty(Exchange))
            {
                _dialogService.ShowMessageDialog("Warning", "Exchange is empty!", null);
                CanExecuteTestConnection = true;
                return;
            }

            try
            {
                var factory = new ConnectionFactory() { HostName = IpAddress };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: Exchange,
                                            type: "direct");
            }
            catch(Exception ex)
            {
                //logging?
                _dialogService.ShowMessageDialog("Error", "Exception: " + ex.Message, null);
                CanExecuteTestConnection = true;
                return;
            }
            CommunicationEnabled = true;
            RaisePropertyChanged(nameof(CommunicationEnabled));
        }
        #endregion

        #region SendMessageCommand
        public DelegateCommand SendMessageCommand { get; private set; }

        private bool _canExecuteSendMessage = true;
        public bool CanExecuteSendMessage
        {
            get { return _canExecuteSendMessage; }
            set { SetProperty(ref _canExecuteSendMessage, value); }
        }

        private void SendMessage()
        {
            CanExecuteSendMessage = false;
            var body = Encoding.UTF8.GetBytes(InputMessage);

            try
            {
                var factory = new ConnectionFactory() { HostName = IpAddress };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.BasicPublish(exchange: Exchange,
                                    routingKey: InputRoutingKey ?? "",
                                    basicProperties: null,
                                    body: body);
                }
            }
            catch(Exception ex)
            {
                //logging?
                _dialogService.ShowMessageDialog("Error", "Exception: " + ex.Message, null);
                CanExecuteTestConnection = true;
                CommunicationEnabled = false;
                RaisePropertyChanged(nameof(CommunicationEnabled));
                return;
            }
            CanExecuteSendMessage = true;
        }
        #endregion

        #region ReceiveMessagesCommand
        public DelegateCommand ReceiveMessagesCommand { get; private set; }

        private bool _canExecuteReceiveMessages = true;
        public bool CanExecuteReceiveMessages
        {
            get { return _canExecuteReceiveMessages; }
            set { SetProperty(ref _canExecuteReceiveMessages, value); }
        }

        private void ReceiveMessages()
        {
            CanExecuteReceiveMessages = false;
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                var connection = factory.CreateConnection();
                if (_channel != null) _channel.Dispose();
                _channel = connection.CreateModel();

                if (string.IsNullOrEmpty(OutputRoutingKeys)) OutputRoutingKeys = string.Empty;
                var routingKeys = OutputRoutingKeys.Split(';');
                var queueName = _channel.QueueDeclare().QueueName;
                foreach (var routingkey in routingKeys)
                {
                    _channel.QueueBind(queue: queueName,
                                      exchange: Exchange,
                                      routingKey: routingkey);
                }

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += HandleMessage;
                _channel.BasicConsume(queue: queueName,
                                         autoAck: true,
                                         consumer: consumer);
            }
            catch(Exception ex)
            {
                //logging?
                _dialogService.ShowMessageDialog("Error", "Exception: " + ex.Message, null);
                CanExecuteTestConnection = true;
                CommunicationEnabled = false;
                return;
            }
            CanExecuteReceiveMessages = true;
        }

        private void HandleMessage(object sender, BasicDeliverEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(() => MessageList.Add(new RabbitMQMessage
                {
                    RoutingKey = e.RoutingKey,
                    Body = Encoding.UTF8.GetString(e.Body.ToArray())
                })));
            
            RaisePropertyChanged(nameof(MessageList));
        }
        #endregion

        #region ClearMessagesCommand
        public DelegateCommand ClearMessagesCommand { get; private set; }

        private void ClearMessages()
        {
            MessageList.Clear();
        }
        #endregion
    }
}