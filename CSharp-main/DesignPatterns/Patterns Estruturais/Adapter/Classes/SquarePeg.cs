namespace TesteDesignPatterns.Patterns_Estruturais.Adapter.Classes
{
    public class SquarePeg
    {
        public int Width { get; set; }

        public SquarePeg(int width)
        {
            Width = width;
        }

        public int getWidth()
        {
            return this.Width;
        }
    }
}
