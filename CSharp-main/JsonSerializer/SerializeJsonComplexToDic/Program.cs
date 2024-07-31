using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        string jsonString =
            "{\"productId\": 12345, \"name\": \"Smartphone XYZ\", \"price\": 999.99, \"details\": {\"brand\": \"XYZ\", \"color\": \"Black\", \"specs\": {\"storage\": \"128GB\", \"ram\": \"8GB\",\"card\": \"8GB\"}}}";

        // Criar instância da classe que contém o dicionário
        MyClass myObject = new MyClass();

        // Deserializar o JSON para o dicionário
        myObject.MyDictionary = DeserializeJson(jsonString);

        // Acessar as propriedades do produto
        Console.WriteLine(myObject.MyDictionary["productId"]); // Saída: 12345
        Console.WriteLine(myObject.MyDictionary["name"]); // Saída: Smartphone XYZ
        Console.WriteLine(myObject.MyDictionary["price"]); // Saída: 999.99

        // Acessar as propriedades dos detalhes do produto
        var details = (Dictionary<string, object>)myObject.MyDictionary["details"];
        Console.WriteLine(details["brand"]); // Saída: XYZ
        Console.WriteLine(details["color"]); // Saída: Black

        // Acessar as propriedades das especificações
        var specs = (Dictionary<string, object>)details["specs"];
        Console.WriteLine(specs["storage"]); // Saída: 128GB
        Console.WriteLine(specs["ram"]); // Saída: 8GB
    }

    public static Dictionary<string, object> DeserializeJson(string jsonString)
    {
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

        foreach (var kvp in dictionary)
        {
            if (kvp.Value is Newtonsoft.Json.Linq.JObject nestedObject)
            {
                dictionary[kvp.Key] = DeserializeJson(nestedObject.ToString());
            }
        }

        return dictionary;
    }
}

public class MyClass
{
    public Dictionary<string, object> MyDictionary { get; set; }
}
