namespace TesteDesignPatterns.Patterns_Estruturais.Adapter.Classes
{
    public class RoundHole
    {
        public int Radius { get; set; }

        public RoundHole(int radius)
        {
            Radius = radius;
        }

        public int getRadius()
        {
            return Radius;
        }

        public bool fits(RoundPeg peg)
        {
            return getRadius() >= peg.GetRadius();
        }
    }
}
