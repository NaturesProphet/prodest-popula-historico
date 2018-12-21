using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace popMQ
{
    class Program
    {
        protected static DAO dao = new DAO();
        protected static string RMQHost = Ambiente.getRabbitHost();
        protected static string RMQTopic = Ambiente.getRabbitTopic();
        protected static string RMQKey = Ambiente.getRabbitKey();
        protected static string RMQChannel = Ambiente.getRabbitChannelName();
        static void Main(string[] args)
        {
            ConnectionFactory factory;
            IConnection connection;
            IModel channel;
            String queueName;
            Console.Clear();

            if (Ambiente.isProduction())
            //modo de produção, saída mínima de texto no terminal.
            {
                try
                {
                    factory = new ConnectionFactory() { HostName = RMQHost };
                    connection = factory.CreateConnection();
                    channel = connection.CreateModel();
                    channel.ExchangeDeclare(exchange: RMQTopic, type: "topic", durable: true);
                    queueName = channel.QueueDeclare(RMQChannel).QueueName;
                    channel.QueueBind(queue: queueName, exchange: RMQTopic, routingKey: RMQKey);
                    X9.ShowInfo(1, null);
                    EventingBasicConsumer consumidorEventos = new EventingBasicConsumer(channel);
                    consumidorEventos.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body;
                        String message = Encoding.UTF8.GetString(body);
                        Historico h = new Historico();
                        h.setJsonRastreio(message);
                        dao.salvaHistorico(h);
                    };
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumidorEventos);
                }
                catch (Exception e)
                {
                    X9.OQueRolouNaParada(e, 1);
                }
            }


            else
            //fora do ambiente de produção. SPAM INFO rola solto
            {
                try
                {
                    factory = new ConnectionFactory() { HostName = RMQHost };
                    connection = factory.CreateConnection();
                    channel = connection.CreateModel();
                    channel.ExchangeDeclare(exchange: RMQTopic, type: "topic", durable: true);
                    queueName = channel.QueueDeclare(RMQChannel).QueueName;
                    channel.QueueBind(queue: queueName, exchange: RMQTopic, routingKey: RMQKey);
                    X9.ShowInfo(2, null);
                    EventingBasicConsumer consumidorEventos = new EventingBasicConsumer(channel);
                    consumidorEventos.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body;
                        String message = Encoding.UTF8.GetString(body);
                        X9.ShowInfo(3, message);
                        Historico h = new Historico();
                        h.setJsonRastreio(message);
                        dao.salvaHistorico(h);
                    };
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumidorEventos);
                }
                catch (Exception e)
                {
                    X9.OQueRolouNaParada(e, 1);
                }
            }
        }
    }
}
