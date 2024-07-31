using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex13
    {
        private static int RolarD6()
        {
            Random random = new Random();
            return random.Next(1,7);
        }

        public static void Resolucao()
        {
            int n = Reader.LerInteiro("Informe a quantidade de vezes que deseja lançar o dado");
            int contador = 0;

            Console.WriteLine();
            for(int i = 0; i < n; i++)
            {
                int rolagem = RolarD6();
                if (rolagem == 6) contador++;
                Console.Write($" {rolagem} |");
            }
            Console.WriteLine($"\n6 foi rolado {contador} vezes");    
        }

    }
}
