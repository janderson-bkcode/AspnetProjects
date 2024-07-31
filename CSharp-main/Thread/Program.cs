using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections;
using System;
using System.Threading.Tasks;

const int NumThreads = 8;
const int NumInterations = 200000;

object lockObject = new object(); // INSTANTICIA DE OBJECT

Console.WriteLine($"POC sem ConcurrentBag");

List<int> list = new List<int>();

Parallel.For(
    0,
    NumInterations,
    new ParallelOptions { MaxDegreeOfParallelism = NumThreads },
    i =>
    {
        lock (lockObject)
        {
            list.Add(i);
        }
    }
);

Console.WriteLine($" Numero de itens na lista {list.Count}");

Console.WriteLine($"POC com ConcurrentBag");
ConcurrentBag<int> bag = new ConcurrentBag<int>();

Parallel.For(
    0,
    NumInterations,
    new ParallelOptions { MaxDegreeOfParallelism = NumThreads },
    i =>
    {
        bag.Add(i);
    }
);
Console.WriteLine($"Numero de Itens na ConcurrentBag {bag.Count}");
Console.ReadLine();
