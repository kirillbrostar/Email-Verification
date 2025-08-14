using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailVerification.Services
{
    public class EmailWorker : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly string _queueName;

        public EmailWorker(IConnectionFactory factory)
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = "email-verification";
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[{DateTime.Now}] Received: {message}");
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}