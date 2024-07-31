using Lista04.Utils;

namespace Lista04.Exercicios
{
    public class Ex04
    {
        private static void ImprimirGrid(int col, int lin)
        {
            for(int i = 0; i < lin; i++)
            {
               
                for(int j = 0; j < col; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public static void Resolucao()
        {
            int col = Reader.LerInteiro("Informe o número de colunas");
            int lin = Reader.LerInteiro("Informe o número de linhas");

            ImprimirGrid(col, lin);
        }
    }
}
