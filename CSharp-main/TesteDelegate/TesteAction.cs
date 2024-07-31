// using System;
// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;
//
// public class Teste
// {
//     public static void Main()
//     {
//         /*	var lista = new[]{"Corinthians","Santos","Palmeiras"};
//            Action<string> minhaAction = new Action<string>(Console.WriteLine);
//
//            //Array.ForEach(lista,Console.WriteLine);
//
//            Array.ForEach(lista,minhaAction);
//            */
//         Clube cl = new Clube();
//
//         var listaTimes = cl.ListarTimes();
//
//         Action<Clube> actionClube = new Action<Clube>(cl.MudarNomeClube);
//
//         listaTimes.ForEach(actionClube);
//
//         foreach (var clube in listaTimes)
//         {
//             Console.WriteLine(clube.Nome);
//         }
//     }
// }
//
// public class Clube
// {
//     public string Nome { get; set; }
//     public string Estadio { get; set; }
//
//     public List<Clube> ListarTimes()
//     {
//         return new List<Clube>() { new Clube { Nome = "Corinthians", Estadio = "Itaquera" }, new Clube { Nome = "Real madrid", Estadio = "Bernabeu" } };
//     }
//
//     public void MudarNomeClube(Clube clube)
//     {
//         clube.Nome = "Barcelona";
//     }
// }
//
// public class TesteAction
// {
//     public void Teste()
//     {
//         List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
//
//         // Declara��o de vari�veis para armazenar os resultados.
//         int sum = 0;
//         int product = 1;
//         int max = int.MinValue;
//
//         // Usando Action para realizar v�rias opera��es na lista.
//         Action<int> operationsAction = (number) =>
//         {
//             sum += number;
//             product *= number;
//             max = Math.Max(max, number);
//         };
//
//         // Executando a a��o em cada n�mero da lista.
//         numbers.ForEach(operationsAction);
//
//         // Imprimindo os resultados.
//         Console.WriteLine($"Soma dos n�meros: {sum}");
//         Console.WriteLine($"Produto dos n�meros: {product}");
//         Console.WriteLine($"Maior n�mero: {max}");
//     }
//
//     public void Teste02()
//     {
//         List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
//
//         // Exemplo usando Action para imprimir cada n�mero da lista.
//         Action<int> printNumberAction = (number) => Console.WriteLine(number);
//
//         // Exemplo usando Action para dobrar cada n�mero da lista.
//         Action<int> doubleNumberAction = (number) => Console.WriteLine($"O dobro de {number} � {number * 2}.");
//
//         // Executando as a��es em cada n�mero da lista.
//         numbers.ForEach(printNumberAction);
//         numbers.ForEach(doubleNumberAction);
//     }
// }