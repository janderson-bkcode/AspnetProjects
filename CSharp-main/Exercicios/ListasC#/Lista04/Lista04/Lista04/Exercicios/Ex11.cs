
using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex11
    {
        private static int RetornaNumDigitos(int num)
        {
            string numString = num.ToString();

            return numString.Length;
        
        }

        public static void Resolucao()
        {
            int numero;
            do
            {
                numero = Reader.LerInteiro("Informe um número positivo");
            } while (numero < 0);

            Console.WriteLine($"O número {numero} tem {RetornaNumDigitos(numero)} digitos");
        }
    }
}
