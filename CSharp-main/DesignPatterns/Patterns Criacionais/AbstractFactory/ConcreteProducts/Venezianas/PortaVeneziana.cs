using TesteDesignPatterns.AbstractFactory.InterfacesProduct;

namespace TesteDesignPatterns.AbstractFactory.ConcreteProducts.Venezianas
{
    public class PortaVeneziana : Porta
    {
        public string Modelo { get; set; }
        public double altura { get; set; }
        public double largura { get; set; }
    }
}
