using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace popMQ
{
    class Program
    {
        protected static int contagemRecebidos = Ambiente.getContagem();
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
            try
            {
                factory = new ConnectionFactory() { HostName = RMQHost };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: RMQTopic, type: "topic", durable: true);
                queueName = channel.QueueDeclare(RMQChannel).QueueName;
                channel.QueueBind(queue: queueName, exchange: RMQTopic, routingKey: RMQKey);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[  POP-MQ  ]   Serviço inicializado!");
                Console.WriteLine("[  POP-MQ  ]   Aguardando a chegada dos dados...\n");
                Console.ResetColor();
                EventingBasicConsumer consumidorEventos = new EventingBasicConsumer(channel);
                consumidorEventos.Received += (model, ea) =>
                {
                    contagemRecebidos++;
                    byte[] body = ea.Body;
                    String message = Encoding.UTF8.GetString(body);
                    if (contagemRecebidos == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("[  POP-MQ  ]   Os dados já estão sendo recebidos...");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.ResetColor();
                    }
                    if (!Ambiente.isProduction())
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("[  POP-MQ  ]   Recebendo dados: " + message);
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.ResetColor();
                    }
                    if (contagemRecebidos % 15000 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("[  POP-MQ  ]   contagem: " + contagemRecebidos + " mensagens");
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.ResetColor();
                    }
                    Historico h = new Historico();
                    h.setJsonRastreio(message);
                    dao.salvaHistorico(h);
                };
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumidorEventos);

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n##########################################################");
                Console.WriteLine("[ POP-MQ  ]   Erro ao tentar se conectar com o RabbitMQ");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("[ ERROR:  ]   " + e.Message);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("[ POP-MQ  ]   Dados da conexão listados a seguir:\n");
                Console.WriteLine("[ POP-MQ  ]   Host: " + RMQHost);
                Console.WriteLine("[ POP-MQ  ]   Topico: " + RMQTopic);
                Console.WriteLine("[ POP-MQ  ]   Chave de Rota: " + RMQKey);
                Console.WriteLine("[ POP-MQ  ]   Canal: " + RMQChannel);
                Console.WriteLine("##########################################################");
                Console.ResetColor();
            }
        }
    }
}
