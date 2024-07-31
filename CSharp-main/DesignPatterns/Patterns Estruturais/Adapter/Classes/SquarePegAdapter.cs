using System;

namespace TesteDesignPatterns.Patterns_Estruturais.Adapter.Classes
{
    public class SquarePegAdapter : RoundPeg
    {
        private SquarePeg SquarePeg { get; set; }
        public int Radius { get; set; }

        public SquarePegAdapter(SquarePeg squarePeg)
        {
            SquarePeg = squarePeg;
        }

        public double GetRadius()
        {
            return SquarePeg.getWidth() * Math.Sqrt(2) / 2;
        }
    }
}
