

using Lista04.Utils;

namespace Lista04.Exercicios
{
    public static class Ex07
    {
        private static string ClassificaNotaAluno(string nome, double nota)
        {
            string msg = $"O aluno {nome} foi classificado como ";

            if (nota <= 5) msg += "insatisfatório";
            else if (nota <= 10) msg += "abaixo da média";
            else if (nota <= 15) msg += "na média";
            else if (nota < 20) msg += "Acima da média";
            else msg += "Excelente!";

            return msg;
        }

        public static void Resolucao()
        {
            for(int i = 0; i < 10; i++)
            {
                string nome = Reader.LerString("Informe o nome do aluno");
                int nota;
                do
                {
                    nota = Reader.LerInteiro("Informa a nota do aluno, entre 0 e 20");

                } while (nota >= 20 || nota < 0);
                Console.WriteLine(ClassificaNotaAluno(nome, nota));
            }
        }
    }
}
