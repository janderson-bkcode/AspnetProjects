

using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex12
    {
        private static int EncontraMaior(int[] array)
        {
            int maior = array[0];

            for(int i = 1; i < array.Length; i++)
            {
                if (array[i] > maior) maior = array[i];
            }

            return maior;
        }
        private static int ContaOcorrenciaMaior(int[] array, int maior)
        {
            int contador = 0;

            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == maior) contador++;
            }

            return contador;
        }
        private static int[] InsereNumSequencia(int n)
        {
            int[] array = new int[n];

            for (int i = 0; i < n; i++)
            {
                array[i] = Reader.LerInteiro($"Informe o {i+1}º número da sequencia");
            }
            return array;
        }

        public static void Resolucao()
        {
            int n = Reader.LerInteiro("Informe a dimensão da sequencia");

            int[] array = InsereNumSequencia(n);

            int maior = EncontraMaior(array);

            int numOcorrencia = ContaOcorrenciaMaior(array, maior);

            Console.WriteLine($"O número {maior} aparece {numOcorrencia} vezes na sequencia");
        }
    }
}
