using Confluent.Kafka;
using Newtonsoft.Json;

namespace ApexInvest.Infrastructure.Messaging;

public class KafkaProducerService
{
    private readonly IConfiguration _configuration;
    private readonly ProducerConfig _config;

    public KafkaProducerService(IConfiguration configuration)
    {
        _configuration = configuration;
        _config = new ProducerConfig { BootstrapServers = "localhost:9092" };
    }

    public async Task SendOrderMessageAsync(string topic, object message)
    {
        using var producer = new ProducerBuilder<Null, string>(_config).Build();

        var jsonMessage = JsonConvert.SerializeObject(message);
        await producer.ProduceAsync(topic, new Message<Null, string> { Value = jsonMessage });
    }
}