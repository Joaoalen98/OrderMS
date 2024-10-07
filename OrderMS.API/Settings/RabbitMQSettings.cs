using System;

namespace OrderMS.API.Settings;

public class RabbitMQSettings
{
    public string HostName { get; set; } = default!;
    public int Port { get; set; }
    public string OrderCreatedQueueName { get; set; } = default!;
    public string User { get; set; } = default!;
    public string Password { get; set; } = default!;
}
