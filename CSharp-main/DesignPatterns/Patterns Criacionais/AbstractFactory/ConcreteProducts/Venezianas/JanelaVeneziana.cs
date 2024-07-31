using TesteDesignPatterns.AbstractFactory.InterfacesProduct;

namespace TesteDesignPatterns.AbstractFactory.ConcreteProducts.Venezianas
{
    public class JanelaVeneziana : Janela

    {
        public bool grade { get; set; }

        public bool mosaico { get; set; }

        public void abrir()
        {
            System.Console.WriteLine("Abrir Janela");
        }

        public void fechar()
        {
            System.Console.WriteLine("Fechando Janela");
        }

        public void Grade()
        {
            System.Console.WriteLine("Possui Grade");
        }


    }
}
