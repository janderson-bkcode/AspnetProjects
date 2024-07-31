using Amazon.Lambda;
using Amazon.Lambda.Model;

AmazonLambdaClient cliente = new AmazonLambdaClient();

const string input = "Texto Ola mundo";
var request = new InvokeRequest()
{
    FunctionName = "Lambda",
    Payload = input
};
var response = cliente.InvokeAsync(request).Result;

// See https://aka.ms/new-console-template for more information
Console.WriteLine($"Result => {new StreamReader(response.Payload).ReadToEnd()}");