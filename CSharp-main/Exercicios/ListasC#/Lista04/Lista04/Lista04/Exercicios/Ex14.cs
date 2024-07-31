using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex14
    {
        private static int CalculaValorFrete(int qtdItens)
        {
            return qtdItens + 4;
        }

        public static void Resolucao()
        {
            int qtdItens = Reader.LerInteiro("Quantos itens serão despachados?");
            Console.WriteLine($"O valor do frete será R$ {CalculaValorFrete(qtdItens).ToString("N2")}");
        }
    }
}
