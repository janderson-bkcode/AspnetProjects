using System;
using System.Text;
using System.Collections.Concurrent;


ConcurrentQueue<int> coll = new ConcurrentQueue<int>();


Task t1 = Task.Factory.StartNew(() =>
    {
        for (int i = 0; i < 100; ++i)
        {
            coll.Enqueue(i);
            Console.WriteLine($"Enfileirando{i}");
            Thread.Sleep(3000);
        }
    });

Task t2 = Task.Factory.StartNew(() =>
    {

        /*
        A fila ConcurrentQueue não suporta iteração direta usando um loop foreach. 
        Portanto, o trecho do código que itera sobre os itens da fila e exibe as mensagens não funcionará corretamente. 
        A iteração ocorre apenas nos itens presentes na fila no momento em que o loop foreach é iniciado.
        Para corrigir esse problema, você pode usar o método ToArray() para obter uma cópia dos itens da 
        fila em uma matriz e, em seguida, iterar sobre essa matriz. Isso garantirá que todos os itens sejam verificados, 
        mesmo que novos itens sejam enfileirados posteriormente.*/

            Thread.Sleep(3000);
            int itemCount = 0;
            while (!coll.IsEmpty)
            {
                if (coll.TryDequeue(out int item))
                {
                    Console.WriteLine($"Verificando item {item}");
                    itemCount++;
                    Thread.Sleep(3000);
                }
            }
            Console.WriteLine($"Verificação concluída. Total de itens verificados: {itemCount}");
    });

try
{
    Task.WaitAll(t1, t2);
}
catch (AggregateException ex) // No exception
{
    Console.WriteLine(ex.Flatten().Message);
}