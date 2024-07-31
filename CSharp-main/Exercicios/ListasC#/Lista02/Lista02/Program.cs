using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista02.Lista02
{

    class Program
    {
        public static void Main(string[] args)
        {
            List02.exec13();

        }
    }

    public class List02
    {

        public static void exec01()
        {

            System.Console.WriteLine($"{4 == 5}");
            System.Console.WriteLine($"{4 != 5}");
            System.Console.WriteLine($"{4 > 5}");
            System.Console.WriteLine($"{true == false}");
            System.Console.WriteLine($"{'a' == 'a'}");
            System.Console.WriteLine($"{'a' == 'b'}");
            System.Console.WriteLine($"{2 < 3 && 3 > 4}");
            System.Console.WriteLine($"{2 < 3 && 3 > 4}");
            System.Console.WriteLine($"{2 < 3 || 3 > 4}");
            System.Console.WriteLine($"{!(2 < 3 || 3 > 4)}");
        }

        public static void exec02()
        {
            double numeroInt = 20.34;
            int convert = Convert.ToInt32(Math.Round(numeroInt));
            System.Console.WriteLine(convert);
        }
        public static void exec03()
        {
            int um_numero;
            Console.WriteLine("Digite Um numero");
            um_numero = int.Parse(Console.ReadLine());
            if (um_numero > 0 && um_numero % 2 == 0)
            {
                Console.WriteLine("Numero Positivo e Par");
            }
            else if (um_numero > 0 && um_numero % 2 != 0)
            {
                System.Console.WriteLine("Numero positivo Impar");
            }
            else
            {
                System.Console.WriteLine("Numero Negativo");
            }

        }

        public static void exec04()
        {
            double horasTrabalhadas, salarioHora;
            System.Console.WriteLine("Digite as horas Trabalhadas");
            horasTrabalhadas = double.Parse(Console.ReadLine());
            System.Console.WriteLine("Digite o valor do Salario por hora");
            salarioHora = double.Parse(Console.ReadLine());

            if (horasTrabalhadas > 40)
            {
                salarioHora *= 2;
                horasTrabalhadas *= salarioHora;
                System.Console.WriteLine($"Valor da Remuneração Dobrada ${horasTrabalhadas} reais");
            }
            else
            {
                horasTrabalhadas *= salarioHora;
                System.Console.WriteLine($"Valor da Remuneração ${horasTrabalhadas} reais");
            }

        }
        public static void exec05()
        {
            int a, b, c, menor, maior;
            a = 2;
            b = 8;
            c = 3;
            menor = a;
            maior = a;

            if (menor > b)
                menor = b;
            if (menor > c)
                menor = c;

            if (maior < b)
                maior = b;
            if (maior < c)
                maior = c;

            System.Console.WriteLine($" Maior {maior} , Menor :{menor}");

        }
        public static void exec06()
        {

            double nota1, nota2, media;
            string nomeAluno;
            System.Console.WriteLine("Digite o nome do Aluno");
            nomeAluno = Console.ReadLine();
            System.Console.WriteLine($"Digite a primeira nota do aluno {nomeAluno}");
            nota1 = double.Parse(Console.ReadLine());
            System.Console.WriteLine($"Digite a Segunda nota do aluno {nomeAluno}");
            nota2 = double.Parse(Console.ReadLine());
            media = (nota1 + nota2) / 2;

            if ((nota1 < 0 || nota1 > 10) || (nota2 < 0 || nota2 > 10))
            {
                System.Console.WriteLine("Valor de uma das notas Invalida digite um valor maior que zero e menor que 10");
            }
            else if (media > 8.0)
            {
                System.Console.WriteLine($"O aluno {nomeAluno} está aprovado com a nota final {media}");
            }
            else
            {
                System.Console.WriteLine($"O aluno {nomeAluno} está Reprovado com a nota final {media}");
            }
        }

        public static void exec07()
        {
            int nota;
            string mensagem;
            System.Console.WriteLine("Digite a nota do Aluno");
            nota = int.Parse(Console.ReadLine());

            mensagem = nota > 10 ? "Parabéns" : "Faça um novo Exame";
            System.Console.WriteLine(mensagem);
        }

        public static void exec08()
        {
            int ano;
            Console.WriteLine("Digite o ano");
            ano = int.Parse(Console.ReadLine());

            if (DateTime.IsLeapYear(ano))
            {
                System.Console.WriteLine($" o ano {ano} é bissexto");
            }
            else
            {
                System.Console.WriteLine($" o ano {ano} não é bissexto");
            }
        }

        // public static void exec09()
        // {
        //     int a, b, c;
        //     Console.WriteLine("Digite o primeiro Numero");
        //     a = int.Parse(Console.ReadLine());
        //     Console.WriteLine("Digite o Segundo Numero");
        //     b = int.Parse(Console.ReadLine());
        //     Console.WriteLine("Digite o Terceiro Numero");
        //     c = int.Parse(Console.ReadLine());

        //     if (a > b)
        //     {
        //         if (b > c) Console.WriteLine(a, b, c);
        //         else
        //         {
        //             if (a > c) Console.WriteLine(a, c, b);
        //             else Console.WriteLine(c, a, b);
        //         }


        //     }
        //     if (b > c)
        //     {
        //         if (a > c) Console.WriteLine(b, a, c);
        //         else Console.WriteLine(b, c, a);
        //     }
        //     else Console.WriteLine(c, b, a);
        // }

        public static void exec10()
        {
            string produto, TipoProduto;
            double valorProduto;
            System.Console.WriteLine("Digite o nome do Produto");
            produto = Console.ReadLine();
            System.Console.WriteLine($"Digite o tipo a Categoria do Produto: {produto}");
            TipoProduto = Console.ReadLine();
            System.Console.WriteLine($"Digite o valor do Produto: {produto}");
            valorProduto = double.Parse(Console.ReadLine());

            switch (TipoProduto)
            {
                case "Essencial":
                    System.Console.WriteLine($"Produto do Tipo Essencial, valor com ICMS ${Math.Round(valorProduto * 1.05, 3)}Reais");
                    break;
                case "Luxo":
                    System.Console.WriteLine($"Produto do Tipo Luxo, valor com ICMS ${Math.Round(valorProduto * 1.30, 3)}Reais");
                    break;
                default:
                    System.Console.WriteLine($"Produto do Tipo Geral, valor com ICMS ${Math.Round(valorProduto * 1.20, 3)}Reais");
                    break;
            }
        }

        public static void exec11()
        {

            int a, b, c;

            System.Console.WriteLine("Digite o Primeiro valor");
            a = int.Parse(Console.ReadLine());
            System.Console.WriteLine("Digite o Segundo valor");
            b = int.Parse(Console.ReadLine());
            System.Console.WriteLine("Digite o Terceiro valor");
            c = int.Parse(Console.ReadLine());

            if (a + b > c && a + c > b && b + c > a)
            {
                Console.WriteLine("Os 3 lados formam um triangulo!\n");
                if (a == b && a == c)
                    Console.WriteLine("Equilatero\n");
                else
                    if (a == b || a == c || b == c)
                    Console.WriteLine("Isosceles\n");
                else
                    Console.WriteLine("Escaleno\n");
            }
            else
                Console.WriteLine("Os 3 lados Não formam um triângulo!\n");

        }

        public static String exec12()
        {
            string[] strUnidades = { "", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove", "dez" };

            for (int i = 0; i < strUnidades.Length; i++)
            {
                System.Console.WriteLine(strUnidades[i]);
            }

        }

        public static void exec13()
        {
            {
                double a, b, c;
                double delta, baskara;

                Console.WriteLine("Entre com o valor de A:");
                a = double.Parse(Console.ReadLine());
                Console.WriteLine("Entre com o valor de B:");
                b = double.Parse(Console.ReadLine());
                Console.WriteLine("Entre com o valor de C:");
                c = double.Parse(Console.ReadLine());

                if (a == 0 && b == 0 && c == 0)
                {
                    Console.WriteLine("A, B e C devem ser diferentes de 0!");
                }
                else
                {
                    delta = Math.Pow(b, 2) * (-4 * a * c);
                    if (delta < 0)
                    {
                        Console.WriteLine("Delta não pode ser menor que 0!");
                    }
                    else if (delta == 0)
                    {
                        baskara = -b / (2 * a);
                    }
                    else
                    {
                        baskara = -b + Math.Sqrt(delta) / (2 * a);
                        Console.WriteLine(String.Format("1º Valor da Equação do segundo grau:{0}", baskara));
                        baskara = -b - Math.Sqrt(delta) / (2 * a);
                        Console.WriteLine(String.Format("2º Valor da Equação do segundo grau:{0}", baskara));
                    }
                }



            }
        }

        public static void exec14()
        {
            var letra;

            Console.WriteLine("Digite um caracter: ");
            letra = Console.ReadLine();
            // a, e, i, o, u, A, E, I, O, U
            if (letra == 'a' || letra == 'A' || letra == 'e' || letra == 'E' || letra == 'i'
               || letra == 'I' || letra == '0' || letra == 'O' || letra == 'u' || letra == 'U')
            {
                Console.WriteLine("\tVogal...\n");
            }

            else
                Console.WriteLine("\tConsoante\n");
        }

        public static void exec15()
        {
            // Elabore um programa que leia o dia e o mes de nascimento de uma pessoa e
            // determine o seu signo conforme a tabela a seguir:
            //
            // Intervalo           Signo
            // de 22/12 ate 20/1   Capricornio
            // de 21/1 ate 19/2    Aquario
            // de 20/2 ate 20/3    Peixes
            // de 21/3 ate 20/4    Aries
            // de 21/4 ate 20/5    Touro
            // de 21/5 ate 20/6    Gemeos
            // de 21/6 ate 21/7    Cancer
            // de 22/7 ate 22/8    Leao
            // de 23/8 ate 22/9    Virgem
            // de 23/9 ate 22/10   Libra
            // de 23/10 ate 21/11  Escorpiao
            // de 22/11 ate 21/12  Sagitario
            int dia, mes;

            Console.WriteLine("Informe o dia de nascimento:\n");
            dia = int.Parse(Console.ReadLine());

            Console.WriteLine("\nInforme o mes de nascimento:\n");
            mes = int.Parse(Console.ReadLine());

            if (((mes == 12) && ((dia >= 22) && (dia <= 31))) ||
                ((mes == 1) && ((dia >= 1) && (dia <= 20))))
                Console.WriteLine("\nCapricornio.");
            else if (((mes == 1) && ((dia >= 21) && (dia <= 31))) ||
                     ((mes == 2) && ((dia >= 1) && (dia <= 19))))
                Console.WriteLine("\nAquario.");
            else if (((mes == 2) && ((dia >= 20) && (dia <= 29))) ||
                     ((mes == 3) && ((dia >= 1) && (dia <= 20))))
                Console.WriteLine("\nPeixes.");
            else if (((mes == 3) && ((dia >= 21) && (dia <= 31))) ||
                     ((mes == 4) && ((dia >= 1) && (dia <= 20))))
                Console.WriteLine("\nAries.");
            else if (((mes == 4) && ((dia >= 21) && (dia <= 30))) ||
                     ((mes == 5) && ((dia >= 1) && (dia <= 20))))
                Console.WriteLine("\nTouro.");
            else if (((mes == 5) && ((dia >= 21) && (dia <= 31))) ||
                     ((mes == 6) && ((dia >= 1) && (dia <= 20))))
                Console.WriteLine("\nGemeos.");
            else if (((mes == 6) && ((dia >= 21) && (dia <= 30))) ||
                     ((mes == 7) && ((dia >= 1) && (dia <= 21))))
                Console.WriteLine("\nCancer.");
            else if (((mes == 7) && ((dia >= 22) && (dia <= 31))) ||
                     ((mes == 8) && ((dia >= 1) && (dia <= 22))))
                Console.WriteLine("\nLeao.");
            else if (((mes == 8) && ((dia >= 23) && (dia <= 31))) ||
                     ((mes == 9) && ((dia >= 1) && (dia <= 22))))
                Console.WriteLine("\nVirgem.");
            else if (((mes == 9) && ((dia >= 23) && (dia <= 30))) ||
                     ((mes == 10) && ((dia >= 1) && (dia <= 22))))
                Console.WriteLine("\nLibra.");
            else if (((mes == 10) && ((dia >= 23) && (dia <= 31))) ||
                     ((mes == 11) && ((dia >= 1) && (dia <= 21))))
                Console.WriteLine("\nEscorpiao.");
            else if (((mes == 11) && ((dia >= 22) && (dia <= 30))) ||
                     ((mes == 12) && ((dia >= 1) && (dia <= 21))))
                Console.WriteLine("\nSagitario.");
            else
                Console.WriteLine("\nErro: dia ou mes de nascimento invalidos !!!");
        }

        public static void exec16()
        {
            float A, P, imc;

            Console.Write("Digite o seu peso:");
            P = float.Parse(Console.ReadLine());


            Console.Write("Digite o seu altura:");
            A = float.Parse(Console.ReadLine());

            imc = (P / (A * A));

            if (imc < 18.5)
            {
                Console.WriteLine("Peso abaixo do normal");
            }
            else if ((imc == 18.5) || (imc == 25))
            {
                Console.WriteLine("Peso normal");
            }
            if ((imc > 25) || (imc == 30))

            {
                Console.WriteLine("Sobre o Peso");
            }
            else if ((imc > 30) || (imc == 35))
            {
                Console.WriteLine("Grau de Obesidade I");
            }
            if ((imc > 35) || (imc == 40))
            {
                Console.WriteLine("Grau de Obesidade II");
            }
            else if (imc > 40)
            {
                Console.WriteLine("Obesidade Grau III");
            }

            Console.ReadKey();
        }

        public static void exec17()
        {
            double numero;
            string mensagem;
            System.Console.WriteLine("Digite um numero");
            numero = double.Parse(Console.ReadLine());
            mensagem = numero % 2 == 1 ? "Numero Impar" : "Numero Par";
            System.Console.WriteLine(mensagem);

        }

        public static void exec18()
        {
            string[] strDias = { "Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sabado" };

            for (int i = 0; i < strDias.Length; i++)
            {
                System.Console.WriteLine(strDias[i]);
            }
        }

        public static void exec19()
        {
            //simples calculadora

            double num1, num2;//variáveis que irão conter os números digitados
            string tmp;

            Console.WriteLine("Digite um número: ");
            tmp = Console.ReadLine();
            num1 = int.Parse(tmp);

            Console.WriteLine("Digite mais um número: ");
            Console.ReadLine();
            num2 = int.Parse(tmp);

            double resultado;

            //realiza a soma e imprime no console
            resultado = num1 + num2;
            Console.WriteLine("Soma: " + resultado);

            //realiza a subtração e imprime no console
            resultado = num1 - num2;
            Console.WriteLine("Subtração: " + resultado);

            //realiza a multiplicação e imprime no console
            resultado = num1 * num2;
            Console.WriteLine("Multiplicação: " + resultado);

            //realiza a divisão e imprime no console
            resultado = num1 / num2;
            Console.WriteLine("Divisão: " + resultado);
        }
        public static void exec20()
        {
            string mes;

            Console.WriteLine("Digite um numero: ");
            mes = Console.ReadLine();

            switch (mes)
            {
                case "Janeiro":
                case "Março":
                case "Maio":
                case "Julho":
                case "Agosto":
                case "Outubro":
                case "Dezembro":
                    Console.WriteLine("\nO mes possui 31 dias\n");
                    break;
                case "Fevereiro":
                    Console.WriteLine("\nO mes possui 28 dias\n");
                    break;
                case "Abril":
                case "Junho":
                case "Setembro":
                case "Novembro":
                    Console.WriteLine("\nO mes possui 30 dias\n");
                    break;
                default:
                    Console.WriteLine("\nValor nao corresponde a nenhum mes!\n");
            }
        }
    }

}


