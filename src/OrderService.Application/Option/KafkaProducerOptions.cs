using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Option
{
    public class KafkaProducerOptions
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string Acks { get; set; } = "All";
        public bool EnableIdempotence { get; set; }
        public int MessageSendMaxRetries { get; set; }
        public int RetryBackoffMs { get; set; }
        public int LingerMs { get; set; }
    }
}
