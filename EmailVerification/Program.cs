using EmailVerification.Models;
using EmailVerification.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация сервисов
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Регистрация сервисов RabbitMQ
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<IConnectionFactory>(sp =>
    new ConnectionFactory
    {
        HostName = builder.Configuration["RabbitMQ:HostName"]
    });
builder.Services.AddSingleton<IEmailQueueService, RabbitMqEmailService>();
builder.Services.AddSingleton<IVerificationCacheService, MemoryCacheService>();
builder.Services.AddHostedService<EmailWorker>();

var app = builder.Build();

// Конфигурация middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactPolicy");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();