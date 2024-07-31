using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;

public class RpcClient : IDisposable
{
    private const string QUEUE_NAME = "rpc_queue";

    private readonly IConnection connection;
    private readonly IModel channel;
    private readonly string replyQueueName;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper = new();

    public RpcClient()
    {
        // Cria uma conexão e um canal para comunicação com o RabbitMQ
        var factory = new ConnectionFactory { HostName = "localhost" };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();

        // Declara uma fila com nome do servidor
        replyQueueName = channel.QueueDeclare(queue: "rpc_ReplyQueue").QueueName;

        // Cria um consumidor para receber mensagens da fila de resposta
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            // Verifica se há uma tarefa de conclusão pendente correspondente à correlação recebida
            if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                return;

            // Lê a resposta recebida e conclui a tarefa de conclusão correspondente com a resposta
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.TrySetResult(response);
        };

        // Inicia o consumo da fila de resposta
        channel.BasicConsume(consumer: consumer,
                             queue: replyQueueName,
                             autoAck: true);
    }

    public Task<string> CallAsync(string message, CancellationToken cancellationToken = default)
    {
        // Cria propriedades básicas para a mensagem
        IBasicProperties props = channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = replyQueueName;

        // Converte a mensagem em bytes
        var messageBytes = Encoding.UTF8.GetBytes(message);

        // Cria uma nova TaskCompletionSource para a correlação atual
        var tcs = new TaskCompletionSource<string>();
        callbackMapper.TryAdd(correlationId, tcs);

        // Publica a mensagem na fila principal
        channel.BasicPublish(exchange: string.Empty,
                             routingKey: QUEUE_NAME,
                             basicProperties: props,
                             body: messageBytes);

        // Registra uma ação para remover a tarefa de conclusão caso o token de cancelamento seja acionado
        cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));

        // Retorna a tarefa de conclusão associada à correlação
        return tcs.Task;
    }

    public void Dispose()
    {
        // Fecha o canal e a conexão com o RabbitMQ
        channel.Close();
        connection.Close();
    }
}

public class Rpc
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("RPC Client");
        string n = args.Length > 0 ? args[0] : "30";

        // Cria um cliente RPC
        using var rpcClient = new RpcClient();

        Console.WriteLine(" [x] Requesting fib({0})", n);

        // Chama o método fib no servidor usando o cliente RPC e espera pela resposta
        var response = await rpcClient.CallAsync(n);

        Console.WriteLine(" [.] Got '{0}'", response);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}