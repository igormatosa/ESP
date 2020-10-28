namespace Aucxis.RabbitMQMonitor.InputOutputModule.Models
{
    public class RabbitMQMessage
    {
        public string Body { get; set; }
        public string RoutingKey { get; set; }
    }
}
