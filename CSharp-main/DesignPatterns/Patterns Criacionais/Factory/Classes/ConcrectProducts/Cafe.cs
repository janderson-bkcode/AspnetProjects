using System;
using TesteDesignPatterns.Factory.InterfacesProduct;

namespace TesteDesignPatterns.Factory.Classes.ConcrectProducts
{
    public class Cafe : Bebida
    {
        public void Preparo()
        {
            Console.WriteLine("Café");
        }
    }
}
