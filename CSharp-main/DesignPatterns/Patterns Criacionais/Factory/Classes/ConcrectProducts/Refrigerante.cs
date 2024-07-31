using TesteDesignPatterns.Factory.InterfacesProduct;

namespace TesteDesignPatterns.Factory.Classes.ConcrectProducts
{
    public class Refrigerante : Bebida
    {
        public void Preparo()
        {
            System.Console.WriteLine("Refrigerante");
        }
    }
}
