

using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex01
    {
        private static void Saudacao(string nome)
        {
            Console.WriteLine($"Olá, {nome}");
        }

        public static void Resolucao()
        {
            string nome = Reader.LerString("Informe seu nome: ");
            Saudacao(nome);
        }
    }
}
