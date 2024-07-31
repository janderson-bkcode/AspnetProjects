using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

const int MESSAGE_COUNT = 50_000;

PublishMessagesIndividually();
PublishMessagesInBatch();
await HandlePublishConfirmsAsynchronously();

static IConnection CreateConnection()
{
    var factory = new ConnectionFactory { HostName = "localhost" };
    return factory.CreateConnection();
}

// Publicação de mensagens individualmente
static void PublishMessagesIndividually()
{
    // Criação da conexão e do canal
    using var connection = CreateConnection();
    using var channel = connection.CreateModel();

    // Declaração de uma fila com nome gerado pelo servidor
    var queueName = channel.QueueDeclare(queue: "Publisher_QueueIndividually").QueueName;
    channel.ConfirmSelect();

    // Início da contagem do tempo

    var stopwatch = Stopwatch.StartNew();

    // Publicação de cada mensagem individualmente
    for (int i = 0; i < MESSAGE_COUNT; i++)
    {
        var body = Encoding.UTF8.GetBytes(i.ToString());
        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
        channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));
    }

    // Fim da contagem do tempo
    stopwatch.Stop();

    Console.WriteLine($"Published {MESSAGE_COUNT:N0} messages individually in {stopwatch.Elapsed.TotalSeconds:N0} seconds");
}

// Publicação de mensagens em lote
static void PublishMessagesInBatch()
{
    // Criação da conexão e do canal
    using var connection = CreateConnection();
    using var channel = connection.CreateModel();

    // Declaração de uma fila com nome gerado pelo servidor
    var queueName = channel.QueueDeclare(queue: "Publisher_Queue_MessagesInBatch").QueueName;

    channel.ConfirmSelect();

    var batchSize = 100;
    var outstandingMessageCount = 0;

    // Início da contagem do tempo
    var stopwatch = Stopwatch.StartNew();

    // Publicação de mensagens em lote
    for (int i = 0; i < MESSAGE_COUNT; i++)
    {
        var body = Encoding.UTF8.GetBytes(i.ToString());
        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
        outstandingMessageCount++;

        // Aguarda a confirmação de um lote de mensagens
        if (outstandingMessageCount == batchSize)
        {
            channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));
            outstandingMessageCount = 0;
        }
    }

    // Aguarda a confirmação das mensagens restantes
    if (outstandingMessageCount > 0)
        channel.WaitForConfirmsOrDie(TimeSpan.FromSeconds(5));

    // Fim da contagem do tempo
    stopwatch.Stop();

    Console.WriteLine($"Published {MESSAGE_COUNT:N0} messages in batch in {stopwatch.Elapsed.TotalSeconds:N0} seconds");
}

static async Task HandlePublishConfirmsAsynchronously()
{
    // Criação de conexão e canal
    using var connection = CreateConnection();
    using var channel = connection.CreateModel();

    // Declaração de uma fila nomeada pelo servidor
    var queueName = channel.QueueDeclare(queue: "HandlePublishConfirmQueue").QueueName;
    channel.ConfirmSelect();

    // Dicionário para acompanhar as confirmações pendentes
    var outstandingConfirms = new ConcurrentDictionary<ulong, string>();

    // Função para limpar as confirmações pendentes
    void CleanOutstandingConfirms(ulong sequenceNumber, bool multiple)
    {
        if (multiple)
        {
            // Remove as confirmações múltiplas do dicionário
            var confirmed = outstandingConfirms.Where(k => k.Key <= sequenceNumber);
            foreach (var entry in confirmed)
                outstandingConfirms.TryRemove(entry.Key, out _);
        }
        else
            outstandingConfirms.TryRemove(sequenceNumber, out _);
    }

    // Manipulador para confirmações bem-sucedidas
    channel.BasicAcks += (sender, ea) => CleanOutstandingConfirms(ea.DeliveryTag, ea.Multiple);

    // Manipulador para confirmações com falha (nack)
    channel.BasicNacks += (sender, ea) =>
    {
        outstandingConfirms.TryGetValue(ea.DeliveryTag, out string? body);
        Console.WriteLine($"Message with body {body} has been nack-ed. Sequence number: {ea.DeliveryTag}, multiple: {ea.Multiple}");
        CleanOutstandingConfirms(ea.DeliveryTag, ea.Multiple);
    };

    // Início da contagem do tempo
    var stopwatch = Stopwatch.StartNew();

    // Loop para publicar mensagens e acompanhar as confirmações de forma assíncrona
    for (int i = 0; i < MESSAGE_COUNT; i++)
    {
        var body = i.ToString();
        outstandingConfirms.TryAdd(channel.NextPublishSeqNo, i.ToString());
        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: Encoding.UTF8.GetBytes(body));
    }

    // Aguarda até que todas as confirmações tenham sido recebidas ou o tempo limite seja atingido
    if (!await WaitUntil(60, () => outstandingConfirms.IsEmpty))
        throw new Exception("All messages could not be confirmed in 60 seconds");

    // Fim da contagem do tempo
    stopwatch.Stop();

    Console.WriteLine($"Published {MESSAGE_COUNT:N0} messages and handled confirm asynchronously {stopwatch.Elapsed.TotalSeconds:N0} seconds");
}

// Função para aguardar até que uma determinada condição seja satisfeita ou o tempo limite seja atingido
static async ValueTask<bool> WaitUntil(int numberOfSeconds, Func<bool> condition)
{
    int waited = 0;
    while (!condition() && waited < numberOfSeconds * 1000)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(100));
        waited += 100;
    }

    return condition();
}