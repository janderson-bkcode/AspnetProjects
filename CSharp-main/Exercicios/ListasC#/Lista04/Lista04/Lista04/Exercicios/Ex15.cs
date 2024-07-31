using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex15
    {
        private static int CalculaPotenciaRecursiva(int basen,int expoente)
        {
            if (expoente == 0) return 1;

            return basen * CalculaPotenciaRecursiva(basen, expoente - 1);
        }

        public static void Resolucao()
        {
            int basen = Reader.LerInteiro("Informe a base");
            int expoente = Reader.LerInteiro("Informe o expoente");

            Console.WriteLine($" {basen}^{expoente} = {CalculaPotenciaRecursiva(basen, expoente)}");
        }
    }
}
