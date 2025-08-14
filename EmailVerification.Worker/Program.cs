using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using EmailVerification.Models;
using EmailVerification.Services;


var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "email_verification",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[{DateTime.Now}] Received: {message}");

 
    Console.WriteLine($"Sending email with code to: {message}");
};

channel.BasicConsume(queue: "email_verification",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();