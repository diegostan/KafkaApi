using System.Text.Json;
using Confluent.Kafka;

namespace KafkaProducer.API
{
    public class ProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly ProducerConfig _config;
        private readonly string _topicName;
        private readonly string _bootStrapServer;
        public ProducerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _bootStrapServer = _configuration.GetSection("KafkaConfig").GetSection("BootStrapServer").Value;
            _topicName = _configuration.GetSection("KafkaConfig").GetSection("TopicName").Value;

            _config = new ProducerConfig() { BootstrapServers = _bootStrapServer };

        }

        public async Task<string> SendMessage(string message)
        {            
            try
            {                
                using (var producer = new ProducerBuilder<Null, string>(config: _config).Build())
                {                    
                    var result = await producer.ProduceAsync(topic: _topicName, message: new() { Value = message });
                    
                    return result.Status.ToString() + " - " + message;
                }
            }
            catch (Exception ex)
            {                
                return $"Falha ao enviar mensagem. Mais detalhes {ex.Message}";
            }
        }
    }
}