using TesteDesignPatterns.Factory.Classes.ConcrectProducts;
using TesteDesignPatterns.Factory.Classes.Creator;
using TesteDesignPatterns.Factory.InterfacesProduct;

namespace TesteDesignPatterns.Factory.Classes.Factorys
{
    public class FactoryRefrigerante : BebidaFactoryCreator
    {
        public override Bebida criarBebida()
        {
            return new Refrigerante();
        }
    }
}
