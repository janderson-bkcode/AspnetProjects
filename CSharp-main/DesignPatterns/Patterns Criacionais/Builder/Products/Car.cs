using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDesignPatterns.Builder.Products
{
    public class Car
    {
        public string Engine { get; set; }

        public string Modelo { get; set; }


        public override string ToString()
        {
            return "Carro ";
        }
    }
}
