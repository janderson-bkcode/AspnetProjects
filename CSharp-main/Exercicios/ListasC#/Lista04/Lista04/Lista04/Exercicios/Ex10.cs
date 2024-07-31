

using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex10
    {
        private static bool isPrimo(int numero)
        {


            for (int i = 2; i < numero; i++)
            {
                if (numero % i == 0) return false;
            }
            return true;
        }

        public static void Resolucao()
        {
            int numero;
            int contador = 0;


            do
            {
                numero = Reader.LerInteiro("Informe a quantidade de primos que quer imprimir");
            } while (numero < 0);

            for (int i = 1; contador < numero; i++)
            {
                if (isPrimo(i))
                {
                    Console.Write($"{i} - ");
                    contador++;
                }
            }
        }
    }
}
