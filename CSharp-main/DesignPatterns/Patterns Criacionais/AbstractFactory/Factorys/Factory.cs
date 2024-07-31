using System;
using System.Collections.Generic;
using System.Text;
using TesteDesignPatterns.AbstractFactory.InterfacesProduct;

namespace TesteDesignPatterns.AbstractFactory.Factorys
{
    public interface Factory
    {
        Janela criarJanela();

        Porta criarPorta();

    }
}
