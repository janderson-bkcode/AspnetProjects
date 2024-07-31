

using Lista04.Exercicios;
using Lista04.Utils;

namespace lista04
{
    class Program
    {
        public static void Main(string[] args)
        {
            int numEx; 
            do
            {
                
                numEx = Reader.LerInteiro("\nInforme um exercicio (1 à 15) - Para encerar digite 0");
                Console.Clear();
                switch (numEx)
                {
                    case 0:
                        Console.WriteLine("Finalizando...");
                        break;
                    case 1:
                        Console.WriteLine("1-)Elabore e teste um método que escreva \"Olá, < nome > \".\n");
                        Ex01.Resolucao();
                        break;
                    case 2:
                        Console.WriteLine("2-) Elabore e teste um método que devolva o resultado da soma de dois números inteiros.\n");
                        Ex02.Resolucao();
                        break;
                    case 3:
                        Console.WriteLine("3-) Elabore e teste um método que imprima na tela uma linha com “n” asteriscos, em que “n” é um número \nfornecido pelo usuário.\n");
                        Ex03.Resolucao();
                        break;
                    case 4:
                        Console.WriteLine("4-) Elabore um programa que utilizando o método do exercício (3), imprima “m” linhas em que “m” é um \nnúmero inteiro fornecido pelo usuário.\n");
                        Ex04.Resolucao();
                        break;
                    case 5:
                        Console.WriteLine("5-) Elabore um programa que simule uma máquina de calcular, de número inteiros, em que cada uma das"+
                                         "operações, soma, subtração, multiplicação e divisão seja implementada através de um método.");
                        Ex05.Resolucao();
                        break;
                    case 6:
                        Console.WriteLine("6-) Elabore e teste um método que retorne o maior entre dois valores.\n");
                        Ex06.Resolucao();
                        break;
                    case 7:
                        Console.WriteLine("6-) Elabore um método que leia e valide a classificação de um aluno numa prova escrita. A nota deverá estar"
                                         +"entre 0(zero) e 20.Elabore um programa que invoque o método e valide a entrada das notas relativas a"+
                                            "10 alunos.");
                        Ex07.Resolucao();
                        break;
                    case 8:
                        Console.WriteLine("8-) Elabore e teste um método que devolva o valor da hipotenusa. O método recebe como parâmetros de"+
                                    "entrada os catetos do triângulo.");
                        Ex08.Resolucao();
                        break;
                    case 9:
                        Console.WriteLine("9-) Geração de números pares");
                        Ex09.Resolucao();
                        break;
                    case 10:
                        Console.WriteLine("10-) Geração de números primos");
                        Ex10.Resolucao();
                        break;
                    case 11:
                        Console.WriteLine("11-) Elabore e teste um método que receba, como argumento, um valor inteiro positivo e devolva o número de dígitos desse valor.");
                        Ex11.Resolucao();
                        break;
                    case 12:
                        Console.WriteLine("12-) Elabore e teste um método que leia um conjunto de números inteiros e devolva o número de vezes que o valor máximo surgiu.A dimensão da sequência é passada por parâmetro.");
                        Ex12.Resolucao();
                        break;
                    case 13:
                        Console.WriteLine("13-) Elabore um método que simule o lançamento de um dado.");
                        Ex13.Resolucao();
                        break;
                    case 14:
                        Console.WriteLine("14-) Elabore e teste um programa que calcule os custos de expedição de uma encomenda");
                        Ex14.Resolucao();
                        break;
                    case 15:
                        Console.WriteLine("15-) Potenciação Recursiva");
                        Ex15.Resolucao();
                        break;
                    default:
                        Console.WriteLine("Exercicio Inválido!");
                        break;
                }
                if(numEx != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                }
                
            } while (numEx != 0);
            
        }
    }
}