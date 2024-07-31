using System;
using System.Collections.Generic;
using System.Text;
using TesteDesignPatterns.Factory.Classes.ConcrectProducts;
using TesteDesignPatterns.Factory.Classes.Creator;
using TesteDesignPatterns.Factory.InterfacesProduct;

namespace TesteDesignPatterns.Factory.Classes.Factorys
{
    public class FactorySuco : BebidaFactoryCreator
    {
        public override Bebida criarBebida()
        {
            return new Suco();
        }
    }
}
