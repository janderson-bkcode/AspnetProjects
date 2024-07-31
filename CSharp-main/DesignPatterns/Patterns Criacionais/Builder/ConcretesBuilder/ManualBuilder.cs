using System;
using System.Collections.Generic;
using System.Text;
using TesteDesignPatterns.Builder.InterfacesProduct;
using TesteDesignPatterns.Builder.Products;

namespace TesteDesignPatterns.Builder.ConcretesBuilder
{
    internal class ManualBuilder : IBuilder
    {
        public Manual Manual { get; set; }
        public void reset()
        {
            throw new NotImplementedException();
        }

        public void setEngine(string engine)
        {
            throw new NotImplementedException();
        }

        public void setGps()
        {
            throw new NotImplementedException();
        }

        public void setSeats(int number)
        {
            throw new NotImplementedException();
        }
    }
}
