

namespace Lista04.Utils
{
    public static class Reader
    {
        public static int LerInteiro(string msg)
        {
            Console.WriteLine(msg);
            int retorno = Convert.ToInt32(Console.ReadLine());
            return retorno;
        }
        public static double LerDouble(string msg)
        {
            Console.WriteLine(msg);
            double retorno = Convert.ToDouble(Console.ReadLine());
            return retorno;
        }
        public static string LerString(string msg)
        {
            Console.WriteLine(msg);
            string retorno = Console.ReadLine();
            return retorno;
        }



    }
}
