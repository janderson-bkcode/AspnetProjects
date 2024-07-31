using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDesignPatterns.Builder.InterfacesProduct
{
    public interface IBuilder
    {
        void reset();
        void setSeats(int number);
        void setEngine(string engine);
        void setGps();


    }
}
