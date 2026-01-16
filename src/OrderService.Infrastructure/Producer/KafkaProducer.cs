using Confluent.Kafka;
using Microsoft.Extensions.Options;
using OrderService.Application.Event;
using OrderService.Application.Option;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.Producer
{
    public class KafkaProducer : IEventPublisher
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer(IOptions<KafkaProducerOptions> options)
        {
            var kafka = options.Value;

            var config = new ProducerConfig
            {
                BootstrapServers = kafka.BootstrapServers,
                Acks = Enum.Parse<Acks>(kafka.Acks, true),
                EnableIdempotence = kafka.EnableIdempotence,
                MessageSendMaxRetries = kafka.MessageSendMaxRetries,
                RetryBackoffMs = kafka.RetryBackoffMs,
                LingerMs = kafka.LingerMs
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task PublishAsync<T>(string topic, T message)
        {
            var json = JsonSerializer.Serialize(message);

            await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = json
            });
        }
    }

}
