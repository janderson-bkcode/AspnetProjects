

using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex05
    {

        private static int Somar(int num1, int num2)
        {
            return num1 + num2;
        }
        private static int Subtrair(int num1, int num2)
        {
            return num1 - num2;
        }
        private static int Multiplicar(int num1, int num2)
        {
            return num1 * num2;
        }
        private static double Dividir(int num1, int num2)
        {
            return num1 / num2;
        }


        public static void Resolucao()
        {
            
            int num1, num2, op;
            int stop;
            do
            {
                num1 = Reader.LerInteiro("Informe o primeiro número");
                op = Reader.LerInteiro("Informe a operação 1 + | 2 - | 3 * | 4 /");
                num2 = Reader.LerInteiro("Informe o segundo  número");
                if (op == 4 && num2 == 0)
                {
                    Console.WriteLine("Impossivel dividir por 0");
                }
                stop = 1;

            } while (stop == 1);

            

            switch (op)
            {
                case 1:
                    Console.WriteLine($"{num1}+{num2} = {Somar(num1, num2)}");
                    break;
                case 2:
                    Console.WriteLine($"{num1}-{num2} = {Subtrair(num1, num2)}");
                    break;
                case 3:
                    Console.WriteLine($"{num1}*{num2} = {Multiplicar(num1, num2)}");
                    break;
                case 4:
                    Console.WriteLine($"{num1}/{num2} = {Dividir(num1, num2)}");
                    break;
                default:
                    Console.WriteLine("Operação Inválida");
                    break;
            }
        }


    }
}
