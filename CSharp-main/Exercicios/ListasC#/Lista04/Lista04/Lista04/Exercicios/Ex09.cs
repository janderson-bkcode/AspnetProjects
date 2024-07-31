

using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex09
    {
        private static bool isPar(int numero)
        {
            if (numero % 2 == 0) return true;
            return false;
        }

        public static void Resolucao()
        {
            int numero;
            int contador = 0;


            do
            {
                numero = Reader.LerInteiro("Informe a quantidade de pares que quer imprimir");
            } while (numero < 0);

            for(int i = 0; contador < numero; i++)
            {
                if (isPar(i))
                {
                    Console.Write($"{i} - ");
                    contador++;
                }
            } 
        }
    }
}
