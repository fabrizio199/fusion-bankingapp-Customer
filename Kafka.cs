using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace ConsoleApp7
{
    public class KafkaDemo
    {
       /*
        private static IHostBuilder CreateHostBuilder (string [] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices(context,collection) =>
            {
                collection.AddHostedService<KafkaProducerHostedService>()
            });
        */
    }

 
    public class KafkaConsumer
    {

        public void ActReceive()
        {
            Receive("Kafka_1");
        }

        public void AnotherProducer(CancellationToken cancellationToken)
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            var producer = new ProducerBuilder<Null, string>(config).Build();
            var value = $"Hello world ";
            producer.ProduceAsync("Kafka_1", new Message<Null, string>()
            {
                Value = value
            }, cancellationToken);
            producer.Flush(TimeSpan.FromSeconds(10));
        }


        public void Receive(string topic)
        {
            var config = new ConsumerConfig
            {
                GroupId = "gid-consumer",
                BootstrapServers = "localhost:9092"
            };

            IConsumer<Null,string> consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe(topic);
            while (true)
            {
                var cr = consumer.Consume();
                Console.WriteLine(cr.Message.Value);
            }
        }
    }

/*
    public class KafkaConsumerHostedService : IHostedService
    {
        private readonly ILogger<KafkaConsumerHostedService> _logger;
        private IConsumer<Null, string> _consumer;
        public KafkaConsumerHostedService(ILogger<KafkaConsumerHostedService> logger)
        {
            _logger = logger;
            var config = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("demo");
            await Task.Run;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer?.Dispose();
            return Task.CompletedTask;
        }
    }
*/
}
