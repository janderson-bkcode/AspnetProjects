using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Exercicios04_07_22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Exec.exec13();
            Console.ReadLine();
        }


    }

    public class Exec
    {
        public static void exec01()
        {

            Console.WriteLine("A soma de 3 +4 é {0}", 3 + 4);
        }

        public static void exec02()
        {
            Console.WriteLine("A divisão de 5 por 2 é {0}", 5 / 2);
        }

        public static void exec03()
        {
            Console.WriteLine("O resto da divisão de 40 por 3 é {0}", 40 % 3);
        }

        public static void exec04()
        {
            Console.WriteLine("Digite o Numero");
            var numero = Console.ReadLine();
            Console.WriteLine($"{numero}");
        }

        public static void exec05()
        {
            var num01 = 4.68805;
            var num02 = 4.8;
            var num03 = 5.9964;
            var num04 = 5.0;

            Console.WriteLine(
                $"{Math.Round(num01, 3)},{Math.Round(num02, 3)},{Math.Round(num03, 3)},{Math.Round(num04, 3)}\n" +
                $"{Math.Round(num01, 2)},{Math.Round(num02, 2)},{Math.Round(num03, 2)},{Math.Round(num04, 2)}\n" +
                $"{Math.Round(num01, 1)},{Math.Round(num02, 1)},{Math.Round(num03, 1)},{Math.Round(num04, 1)}"
                );
        }

        public static void exec06()
        {
            double num01 = 4.68805 / 100;
            double num02 = 4.8 / 100;
            double num03 = 5.9964 / 100;
            double num04 = 5.0 / 100;


            Console.WriteLine(num01.ToString(), num02.ToString(), num03.ToString(), num04.ToString());
        }

        public static void exec07()
        {
            Console.WriteLine("Digite 3 numeros");
            var num1 = Console.ReadLine();
            var num2 = Console.ReadLine();
            var num3 = Console.ReadLine();

            Console.WriteLine($"{Math.Round(double.Parse(num1), 10)},{Math.Round(double.Parse(num2), 10)},{Math.Round(double.Parse(num3), 10)}".PadRight(15));
        }

        public static void exec08()
        {
            Console.WriteLine("Digite 3 numeros");
            var num1 = Console.ReadLine();
            var num2 = Console.ReadLine();
            var num3 = Console.ReadLine();

            Console.WriteLine($"{Math.Round(double.Parse(num1), 10)},{Math.Round(double.Parse(num2), 10)},{Math.Round(double.Parse(num3), 10)}".PadLeft(15));
        }

        public static void exec09()
        {

            Console.WriteLine("Digite seu nome");
            var nome = Console.ReadLine();
            Console.WriteLine($"Bom dia {nome}");
        }

        public static void exec10()
        {
            Console.WriteLine("Digite seu nome");
            var nome = Console.ReadLine();
            Console.WriteLine("Digite seu Apelido");
            var apelido = Console.ReadLine();
            Console.WriteLine($"Seu nome é {nome} , Teu Apelido é :{apelido}  ");
        }
        public static void exec11()
        {
            Console.WriteLine("Digite o primeiro numero");
            double numero = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Digite o segundo numero");
            double numero2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"A soma dos dois numeros é {numero + numero2}");

        }

        public static void exec12()
        {
            Console.WriteLine("Digite o valor do lado do Quadrado");
            double lado = Convert.ToDouble(Console.ReadLine());
            double area = lado * lado;
            double perimetro = lado * 4;
            Console.WriteLine($"A área do quardo é {area} e o Perimetro é {perimetro}");

        }

        public static void exec13()
        {
            Console.WriteLine("Digite os dois lados");
            double lado1 = Convert.ToDouble(Console.ReadLine());
            double lado2 = Convert.ToDouble(Console.ReadLine());
            double hipotenusa = (Math.Sqrt(lado1 * lado1) + Math.Sqrt(lado2 * lado2));

            Console.WriteLine($"Hipotenusa é {hipotenusa}");
        }
        public static void exec14()
        {
            Console.Write("Valor em Dollar? ");
            float dolar = float.Parse(Console.ReadLine());

            double euro = dolar / 0.97;

            Console.WriteLine("$ = " + euro);

        }

        public static void exec15()
        {
            double graus_celsius, graus_fahrenheit;
            Console.Write("Digite o valor em Fahrenheit: ");
            graus_fahrenheit = double.Parse(Console.ReadLine());
            graus_celsius = 5 / 9 * (graus_fahrenheit - 32);
            Console.WriteLine("O valor de fahrenheit para Celsius: " + graus_celsius);
            Console.WriteLine();
            Console.Write("Pressione qualquer chave para terminar . . . ");
            Console.ReadKey();
        }

        public static void exec16()
        {
            double valor;
            double taxa = 1.23;
            Console.WriteLine("Digite o valor do Produto");
            valor = double.Parse(Console.ReadLine());
            Console.WriteLine($"O valor do produto com imposto é {valor * taxa}");

        }

        public static void exec17()
        {
            double valor;
            double taxa;
            Console.WriteLine("Digite o valor do Produto");
            valor = double.Parse(Console.ReadLine());
            Console.WriteLine("Digite o valor da taxa");
            taxa = double.Parse(Console.ReadLine());
            Console.WriteLine($"O valor do produto com imposto é {valor * taxa}");
        }

        public static void exec18()
        {
            int Tempo, Horas, Minutos, Segundos;

            Console.WriteLine("Digite o valor em Segundos: ");

            Tempo = int.Parse(Console.ReadLine());

            Horas = Tempo / 3600;

            Minutos = Tempo % 3600 / 60;

            Segundos = Tempo % 60;

            Console.WriteLine("O valor convertido é: " + Horas + ":" + Minutos + ":" + Segundos + " H:M:S");

            Console.WriteLine("Aperta enter para sair");

            Console.ReadKey();
        }

        public static void exec19()
        {
            int idade;
            Console.WriteLine("Digite sua idade");
            idade = int.Parse(Console.ReadLine());
            Console.WriteLine($"Daqui a 20 anos você terá {idade + 20}");
        }

        public static void exec20()
        {
            string linha;
            double media_aproveitamento;
            int nota1, nota2;

            Console.Write("Introduza o nome do aluno = ");
            linha = Console.ReadLine();

            Console.Write("Introduza a nota1 = ");
            linha = Console.ReadLine();
            nota1 = Int32.Parse(linha);

            Console.Write("Introduza a nota2 = ");
            linha = Console.ReadLine();
            nota2 = Int32.Parse(linha);

            media_aproveitamento = (nota1 + nota2) / 2;
            Console.WriteLine("Nota1= " + nota1 + " Nota2= " + nota2);
            Console.WriteLine("Media de aproveitamento= " + media_aproveitamento);
        }

        // public static void exec21()
        // {
        //     int dias = 4;
        //     System.Console.WriteLine("Digite o valor da despesa");
        //     valor = double.Parse(Console.ReadLine());
        //     for (var i = 0; i < dias; i++)
        //     {
        //         valor * 1.20;
        //     }
        //     valor = valor / dias;
        //     System.Console.WriteLine($"{Math.Round(valor, 0)}");
        // }

        public static void exec22()
        {

        }
        public static void exec23()
        {

            int a, b;
            a = 20;
            b = 10;
            a = a + b;
            b = a - b;
            a = a - b;

            Console.WriteLine($"Valor a {a} valor b{b}");

        }


        public static void exec24()
        {
            int antecessor, sucessor, um_numero;
            Console.Write("Digite o valor do um numero: ");
            um_numero = int.Parse(Console.ReadLine());
            antecessor = um_numero - 1;
            sucessor = um_numero + 1;
            Console.WriteLine("O valor do antecessor: " + antecessor);
            Console.WriteLine("O valor do sucessor: " + sucessor);
            Console.WriteLine();
            Console.Write("Pressione qualquer chave para terminar . . . ");
            Console.ReadKey();
        }

        public static void exec25()
        {
            double x1, x2, y1, y2;
            x1 = 12d;
            x2 = 13d;
            y1 = 11d;
            y2 = 10d;
            var distance = Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
            Console.WriteLine(distance);
        }
    }
}