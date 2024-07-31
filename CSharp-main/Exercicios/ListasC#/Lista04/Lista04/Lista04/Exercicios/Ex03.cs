using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex03
    {
        private static void ImprimirLinha(int col)
        {
            for (int j = 0; j < col; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();    
        }

        public static void Resolucao()
        {
            int col = Reader.LerInteiro("Informe o número de colunas");

            ImprimirLinha(col);
        }
    }
}
