namespace EmailVerification.Models
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; } = "localhost";
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string QueueName { get; set; } = "email-verification";
    }
}