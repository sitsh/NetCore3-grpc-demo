using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GRpc.NetCoreBackgroundHostServer.BackgroundServices
{
    public class ComsumeRabbitMQHostedService : BackgroundService
    {
        //private readonly ILogger _logger;
        //private readonly AppSettings _settings;
        //private IConnection _connection;
        //private IModel _channel;
        private readonly IConfiguration _conf;
        public ComsumeRabbitMQHostedService(IConfiguration conf)
        {
            //this._logger = loggerFactory.CreateLogger<ComsumeRabbitMQHostedService>();
            //this._settings = options.Value;
            //InitRabbitMQ(this._settings);
        }

        private void InitRabbitMQ(IConfiguration conf)
        {
            //var factory = new ConnectionFactory { HostName = settings.HostName, };
            //_connection = factory.CreateConnection();
            //_channel = _connection.CreateModel();

            //_channel.ExchangeDeclare(_settings.ExchangeName, ExchangeType.Topic);
            //_channel.QueueDeclare(_settings.QueueName, false, false, false, null);
            //_channel.QueueBind(_settings.QueueName, _settings.ExchangeName, _settings.RoutingKey, null);
            //_channel.BasicQos(0, 1, false);

            //_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //stoppingToken.ThrowIfCancellationRequested();

            //var consumer = new EventingBasicConsumer(_channel);
            //consumer.Received += (ch, ea) =>
            //{
            //    var content = System.Text.Encoding.UTF8.GetString(ea.Body);
            //    HandleMessage(content);
            //    _channel.BasicAck(ea.DeliveryTag, false);
            //};

            //consumer.Shutdown += OnConsumerShutdown;
            //consumer.Registered += OnConsumerRegistered;
            //consumer.Unregistered += OnConsumerUnregistered;
            //consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            //_channel.BasicConsume(_settings.QueueName, false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            Serilog.Log.Information($"consumer received {content}");
        }

        //private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { ... }
        //private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { ... }
        //private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { ... }
        //private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { ... }
        //private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { ... }

        public override void Dispose()
        {
            //_channel.Close();
            //_connection.Close();
            base.Dispose();
        }
    }
}
