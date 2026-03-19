using Confluent.Kafka;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;

namespace ApexInvest.Infrastructure.Messaging;

public class TaxConsumerService : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly ConsumerConfig _config;

    public TaxConsumerService(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "tax-calculation-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<Null, string>(_config).Build();
        consumer.Subscribe("purchase-orders");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                var orderData = JsonConvert.DeserializeObject<dynamic>(result.Message.Value);
                using (var scope = _serviceProvider.CreateScope())
                {
                    Console.WriteLine($"[TAX ENGINE] Processando imposto para Ticker: {orderData.Ticker}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar mensagem do Kafka: {ex.Message}");
            }

            await Task.Yield();
        }
    }
}