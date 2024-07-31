

using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex08
    {
        private static double CalculaHipotenusa(double catop, double catad)
        {
            return Math.Sqrt(Math.Pow(catop, 2) + Math.Pow(catad, 2));
        }

        public static void Resolucao()
        {
            double catop = Reader.LerDouble("Informe o cateto oposto");
            double catad = Reader.LerDouble("Informe o cateto adjacente");

            Console.WriteLine($"O Valor da Hipotenusa é {CalculaHipotenusa(catop, catad)}");
        }
    }
}
