using System;
using System.Collections.Generic;
using System.Text;
using TesteDesignPatterns.Factory.InterfacesProduct;

namespace TesteDesignPatterns.Factory.Classes.ConcrectProducts
{
    public class Suco : Bebida
    {
        public void Preparo()
        {
            Console.Write("Preparando suco");
        }
    }
}
