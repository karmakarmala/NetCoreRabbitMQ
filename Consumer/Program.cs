using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueName = "deviceStatus";
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            int messageCount = Convert.ToInt16(channel.MessageCount(queueName));
            Console.WriteLine(" Listening to the queue. This channels has {0} messages on the queue", messageCount);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Device Status received: {message}");
                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                Thread.Sleep(1000);

            };

            Thread.Sleep(1000 * messageCount);
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            Console.WriteLine(" Connection closed, no more messages.");
            Console.ReadKey();

        }
    }
}
