using System;
using TesteDesignPatterns.Builder;
using TesteDesignPatterns.Builder.ConcretesBuilder;
using TesteDesignPatterns.Builder.Products;

namespace TesteDesignPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Director diretor = new Director();

            CarBuilder carBuilder = new CarBuilder();

            diretor.MakeCarSport(carBuilder,"Engine");

            diretor.MakeCarSuv(carBuilder);

            Car carro = new Car();

            carro = carBuilder.GetResult();

            Console.WriteLine(carro);

        }
    }
}
