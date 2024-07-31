using System;
using System.Collections.Generic;
using System.Text;
using TesteDesignPatterns.AbstractFactory.ConcreteProducts.Venezianas;
using TesteDesignPatterns.AbstractFactory.InterfacesProduct;

namespace TesteDesignPatterns.AbstractFactory.Factorys
{
    public class FactoryVenezianas : Factory
    {
        public Janela criarJanela()
        {
            return new JanelaVeneziana();
        }


        public Porta criarPorta()
        {
            return new PortaVeneziana();      
        
        }
    }
}
