using RabbitMQ.Client;
using System.Text;

namespace EmailVerification.Services
{
    public class RabbitMqEmailService : IEmailQueueService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "email-verification";

        public RabbitMqEmailService(IConnectionFactory factory)
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false);
        }

        public Task<bool> SendVerificationCodeAsync(string email)
        {
            var code = new Random().Next(100000, 999999).ToString();
            var message = $"{email}|{code}";
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: _queueName, body: body);
            Console.WriteLine($"[{DateTime.Now}] Sent code {code} to {email}");

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}