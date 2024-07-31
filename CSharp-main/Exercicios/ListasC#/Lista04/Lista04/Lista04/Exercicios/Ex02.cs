using Lista04.Utils;


namespace Lista04.Exercicios
{
    public static class Ex02
    {
        private static int Somar(int num1, int num2)
        {
            return num1 + num2;
        }

        public static void Resolucao()
        {
            int num1 = Reader.LerInteiro("Informe o primeiro número");
            int num2 = Reader.LerInteiro("Informe o segundo número");
            Console.WriteLine($"{num1}+{num2} = {Somar(num1,num2)}");
        }
    }
}
