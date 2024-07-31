using TesteDesignPatterns.Factory.Classes.ConcrectProducts;
using TesteDesignPatterns.Factory.Classes.Creator;
using TesteDesignPatterns.Factory.InterfacesProduct;

namespace TesteDesignPatterns.Factory.Classes.Factorys
{
    public class FactoryCafé : BebidaFactoryCreator
    {
        public override Bebida criarBebida()
        {
            return new Cafe();
        }

    }
}
