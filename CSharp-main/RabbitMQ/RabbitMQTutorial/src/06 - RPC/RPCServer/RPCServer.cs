using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Declaração da fila "rpc_queue"
channel.QueueDeclare(queue: "rpc_queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

// Configuração do consumo de apenas uma mensagem por vez
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

// Criação de um consumidor para a fila "rpc_queue"
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: "rpc_queue",
                     autoAck: false,
                     consumer: consumer);

Console.WriteLine(" [x] Awaiting RPC requests");

consumer.Received += (model, ea) =>
{
    string response = string.Empty;

    // Obtém o corpo, as propriedades e as propriedades de resposta da mensagem recebida
    var body = ea.Body.ToArray();
    var props = ea.BasicProperties;
    var replyProps = channel.CreateBasicProperties();
    replyProps.CorrelationId = props.CorrelationId;

    try
    {
        // Extrai a mensagem e realiza o cálculo do número Fibonacci
        var message = Encoding.UTF8.GetString(body);
        int n = int.Parse(message);
        Console.WriteLine($" [.] Fib({message})");
        response = Fib(n).ToString();
    }
    catch (Exception e)
    {
        // Tratamento de exceção, caso ocorra algum erro no cálculo do número Fibonacci

        Console.WriteLine($" [.] {e.Message}");
        response = string.Empty;
    }
    finally
    {
        // Converte a resposta em bytes e a publica na fila de resposta específica
        var responseBytes = Encoding.UTF8.GetBytes(response);
        channel.BasicPublish(exchange: string.Empty,
                             routingKey: props.ReplyTo,
                             basicProperties: replyProps,
                             body: responseBytes);

        // Confirma o recebimento da mensagem original
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }
};

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

// Função para cálculo do número Fibonacci (implementação recursiva)
static int Fib(int n)
{
    if (n is 0 or 1)
    {
        return n;
    }

    var fib = Fib(n - 1) + Fib(n - 2);
    return fib;
}