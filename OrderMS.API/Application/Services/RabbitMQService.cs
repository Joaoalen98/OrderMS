using System.Text;
using Newtonsoft.Json;
using OrderMS.API.Application.DTOs;
using OrderMS.API.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderMS.API.Application.Services;

public class RabbitMQService : BackgroundService
{
    private readonly ILogger<RabbitMQService> _logger;
    private readonly IOrderService _orderService;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queue = "order-created";

    public RabbitMQService(ILogger<RabbitMQService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        using var scope = serviceScopeFactory.CreateScope();
        _logger = logger;
        _orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Waiting for messages...");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnReceived;

        _channel.BasicConsume(queue: _queue, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    private async void OnReceived(object? model, BasicDeliverEventArgs ea)
    {
        try
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("Received \n {message}", message);

            var deserialized = JsonConvert.DeserializeObject<Pedido>(message);

            var order = new OrderDTO(
                deserialized!.CodigoPedido,
                deserialized.CodigoCliente,
                deserialized.Itens.Select(x => new OrderItemDTO(x.Produto, x.Quantidade, x.Preco))
            );

            await _orderService.Create(order);
            _logger.LogInformation("Message saved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on parsing message - {error}", ex.Message);
        }
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}

public record Pedido(
    int CodigoPedido,
    int CodigoCliente,
    List<Item> Itens
);

public record Item(
    string Produto,
    int Quantidade,
    double Preco
);
