using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicios04_07_22
{
    public class Metodos
    {
        public static int LerInteiro(string msg)
        {
            string input;
            Console.WriteLine(msg);
            input = Console.ReadLine();

            return Convert.ToInt32(input);
        }
        public static double LerDouble(string msg)
        {
            string input;
            Console.WriteLine(msg);
            input = Console.ReadLine();

            return Convert.ToDouble(input);
        }

        public static string LerString(string msg)
        {
            string input;
            Console.WriteLine(msg);
            input = Console.ReadLine();

            return input;
        }
    }
}
