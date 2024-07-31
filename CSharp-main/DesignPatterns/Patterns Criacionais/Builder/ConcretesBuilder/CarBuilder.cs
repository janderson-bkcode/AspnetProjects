using TesteDesignPatterns.Builder.InterfacesProduct;
using TesteDesignPatterns.Builder.Products;

namespace TesteDesignPatterns.Builder.ConcretesBuilder
{
    public class CarBuilder : IBuilder
    {
        private Car Car { get; set; }

        public void reset()
        {
            Car = new Car();
        }

        public void setEngine(string engine = "engine")
        {
            System.Console.WriteLine(engine);
        }

        public void setGps()
        {
            System.Console.WriteLine("Gps");
        }

        public void setSeats(int number)
        {
            System.Console.WriteLine("number");
        }

        public Car GetResult()
        {
            return Car;
        }
    }
}
