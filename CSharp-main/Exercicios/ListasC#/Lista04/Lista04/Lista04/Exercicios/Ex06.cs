

using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex06
    {
        private static int RetornaMaiorValor(int a, int b)
        {
            if (a > b) return a;
            return b;
        }

        public static void Resolucao()
        {
            int a = Reader.LerInteiro("Informe o primeiro valor");
            int b = Reader.LerInteiro("Informe o segundo valor");

            Console.WriteLine($"O Maior valor entre {a} e {b} é {RetornaMaiorValor(a, b)}");

        }
    }
}
