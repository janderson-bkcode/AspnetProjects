using System;
namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ex001();
        }
        public static void ex001()
        {
            int l, c;
            Console.Write("Informe  a largura: ");
            l = Convert.ToInt32(Console.ReadLine());
            Console.Write("Informe  o comprimento: ");
            c = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < c; j++)
                    Console.Write("X");
                Console.WriteLine();
            }
        }
        public static void ex002()
        {
            for (int i = 1; i <= 50; i += 2)
                Console.WriteLine(i);
        }
        public static void ex003()
        {
            int i, soma;
            soma = 0;
            for (i = 4; i < 100; i += 3)
                soma += i;
            Console.WriteLine($"Soma:{soma}");
            soma = 0;
            i = 4;
            while (i < 100)
            {
                soma += i;
                i += 3;
            }
            Console.WriteLine($"Soma:{soma}");
            soma = 0;
            do
            {
                soma += i;
                i += 3;
            } while (i < 100);
            Console.WriteLine($"Soma:{soma}");
        }
        public static void ex004()
        {
            int inf, sup, i;
            Console.Write("Introduza o limite inferior do intervalo: ");
            inf = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introduza o limite superior do intervalo: ");
            sup = Convert.ToInt32(Console.ReadLine());
            for (i = inf; i <= sup; i++)
                Console.Write($"{i} ");
            Console.WriteLine();
        }
        public static void ex005()
        {
            int inf, sup, i, soma = 0;
            Console.Write("Introduza o limite inferior do intervalo: ");
            inf = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introduza o limite superior do intervalo: ");
            sup = Convert.ToInt32(Console.ReadLine());
            for (i = inf; i <= sup; i++)
            {
                Console.Write($"{i}");
                if (i != sup)
                    Console.Write("+");
                soma += i;
            }
            Console.Write($"={soma}\n");
        }

        public static void ex006()
        {

            int b, e, i, pot = 1; // o elemento neutro da multiplicação é 1
            Console.Write("Introduza o valor da base: ");     // Não é feita validação, ou seja, o utilizador pode  introduzir 
            b = Convert.ToInt32(Console.ReadLine());            //valores incorrectos ou caracteres inválidos e  como tal gerar excepções (ERROS)
            Console.Write("Introduza o valor do expoente: ");
            e = Convert.ToInt32(Console.ReadLine());
            for (i = 0; i < e; i++)
                pot *= b; // ou pot= pot*b;
            Console.Write("{0}^{1}={2}\n", b, e, pot);

        }
        public static void ex007()
        {
            int n, max, min;
            Console.Write("Introduza um nº inteiro (zero para sair): ");
            n = Convert.ToInt32(Console.ReadLine());
            max = n;
            min = n;
            while (n != 0)
            {
                if (n > max)
                    max = n;
                if (n < min)
                    min = n;
                Console.Write("Introduza um nº inteiro (zero para sair): ");
                n = Convert.ToInt32(Console.ReadLine());
            }
            if (max == 0 && min == 0) // Se max e min têm o valor zero quer dizer que o primeiro nº digitado foi zero, ou seja, a condição de saída
                Console.WriteLine("Não foi introduzido nenhum número");
            else
                Console.WriteLine("Máximo: {0}\nMínimo: {1}", max, min);
        }

        public static void exec008()
        {

            int n, soma = 0;
            do
            {
                Console.Write("Introduza um nº inteiro (zero para sair): ");
                n = Convert.ToInt32(Console.ReadLine());
                if (n > 0)
                    soma += n;
            } while (n != 0);
            Console.WriteLine("Soma dos inteiros positivos: {0}", soma);
        }
        public static void exec009()
        {
            int n, a, i, max, min, soma = 0;
            Console.Write("Quantos alunos tem a turma: ");
            n = Convert.ToInt32(Console.ReadLine());
            if (n > 0)
            {
                Console.Write("Introduza a altura do aluno em cm: ");
                a = Convert.ToInt32(Console.ReadLine());
                max = a;
                min = a;
                soma += a;
                for (i = 1; i < n; i++)
                {
                    Console.Write("Introduza a altura do aluno em cm: ");
                    a = Convert.ToInt32(Console.ReadLine());
                    if (a > max)
                        max = a;
                    if (a < min)
                        min = a;
                    soma += a;
                }
                Console.WriteLine("Máximo: {0}\nMínimo: {1}\nSoma: {2}\nMédia: {3}", max, min, soma, soma / (float)n); // O type cast (float)n garante que a divisão é real
            }
            else
                Console.WriteLine("A turma não tem alunos");
        }

        public static void exec010()
        {
            int n, soma = 0;
            Console.Write("Introduza um número inteiro: ");
            n = Convert.ToInt32(Console.ReadLine());
            while (n != 0) // Sugestão: Faça execução passo a passo (F10) e observe os valores das variáveis na janela Locais
            {
                soma += n % 10; // O resto da divisão por 10 permite obter cada um dos dígitos do número
                n /= 10; // Depois da obtenção esse dígito é descartado
            }
            Console.WriteLine("Soma dos dígitos:{0}", soma);
        }
        public static void exec011()
        {
            int i, j, num, r = 0;
            Console.Write("Digite um nº:");
            num = Convert.ToInt32(Console.ReadLine());
            for (i = 1; i <= 5; i++)
            {
                for (j = 1; j <= 5; j++)
                    Console.Write("{0,5} ", r += num);
                Console.WriteLine();
            }
        }

        public static void exec012()
        {
            int n, i;
            Console.Write("Digite um nº inteiro:");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("*** Divisores de {0} ***", n);
            for (i = 1; i <= Math.Sqrt(n); i++) // Podemos encontrar todos os divisores de um número testando até à raiz quadrada desse número
                if (n % i == 0)
                    if (i == n / i)
                        Console.WriteLine(" {0,6}", i); // 1 divisor: i é a raiz quadrada de n
                    else
                        Console.WriteLine("{0,5} {1,5}", i, n / i); // 2 divisores: i e n/i
        }
        public static void exec013()
        {
            int N, n = 1, m = 1;
            float soma = 0;
            Console.Write("Quantos termos? ");
            N = Convert.ToInt32(Console.ReadLine());
            Console.Write("S=");
            do
            {
                soma += (float)n / m; // ou soma=soma + (float)n/m
                Console.Write("{0}/{1}+", n, m);
                n++;
                m = m + 2;// ou m+=2
            } while (n < N);
            Console.WriteLine("{0}/{1}\nsoma={2}", n, m, soma += (float)n / m);
        }

        public static void exec014()
        {
            int N, n = 1, i;
            float soma = 0;
            Console.Write("Quantos termos? ");
            N = Convert.ToInt32(Console.ReadLine());
            if (N > 0)
            {
                Console.Write("S={0}/{1}", n, n + 2);
                soma += (float)n / (n + 2);
                for (i = 1; i < N; i++)
                {
                    n += 2;
                    if (i % 2 == 0)
                    {
                        soma += (float)n / (n + 2);
                        Console.Write("+{0}/{1}", n, n + 2);
                    }
                    else
                    {
                        soma -= (float)n / (n + 2);
                        Console.Write("-{0}/{1}", n, n + 2);
                    }
                }
                Console.WriteLine("\nSoma={0}", soma);
            }
        }

        public static void exec015()
        {
            int N, n;
            double soma = 0;
            Console.Write("Quantas noites:");
            N = Convert.ToInt32(Console.ReadLine());
            Console.Write("S=");
            for (n = 1; n < N; n++)
            {
                soma += 50F / n;
                Console.Write("{0}/{1}+", 50, n);
            }
            Console.WriteLine("{0}/{1}\nSoma={2}", 50, n, soma += 50F / n);
        }

        public static void exec016()
        {
            int N, n, i, cont = 0;
            Console.Write("Introduza o valor de N:");
            N = Convert.ToInt32(Console.ReadLine());
            for (n = 2; n <= N; n++)
            {
                for (i = 2; i <= Math.Sqrt(n); i++)
                {
                    cont++;
                    if (n % i == 0)
                        break;
                }
                if (i > Math.Sqrt(n))
                    Console.Write(" {0}", n);
            }
            Console.WriteLine("\nForam efectuadas {0} divisões", cont);
        }

        public static void exec017()
        {
            int N, n, i;
            Console.Write("Altura da árvore:");
            N = Convert.ToInt32(Console.ReadLine());
            for (n = 0; n < N; n++) // Copa
            {
                for (i = 0; i < N - n; i++)
                    Console.Write(" ");
                for (i = 0; i < 2 * n + 1; i++)
                    Console.Write("*");
                for (i = 0; i < N - n; i++)
                    Console.Write(" ");
                Console.WriteLine();
            }
            for (n = 0; n < N / 3; n++) // Tronco (1/3 da copa)
            {
                for (i = 0; i < 2 * N; i++)
                    if (i == N)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                Console.WriteLine();
            }
        }
        public static void ex018()
        {
            int valor1 = 147;
            int valor2 = 205;
            while (valor1 != 0 && valor2 != 0)
            {
                if (valor1 > valor2)
                    valor1 -= valor2;
                else
                    valor2 -= valor1;
            }
            int Mdc = Math.Max(valor1, valor2);
            Console.WriteLine($"O MDC de {valor1} e {valor2} é {Mdc}");
        }
        public static void ex019()
        {
            int a, b, c, d, r;
            Console.Write("Introduza o primeiro número:");
            a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Introduza o segundo número:");
            b = Convert.ToInt32(Console.ReadLine());
            c = a; d = b;
            while ((r = a % b) != 0) // Calcula o máximo divisor comum
            {
                a = b;
                b = r;
            }
            Console.WriteLine("O mínimo múltiplo comum de {0} e {1} é {2}", c, d, c * d / b);
        }
        public static void ex020()
        {
            int n, idade, i, soma = 0;
            Console.Write("Quantos alunos tem a turma: ");
            n = Convert.ToInt32(Console.ReadLine());
            if (n > 0)
            {
                for (i = 1; i < n; i++)
                {
                    Console.Write("Introduza a idade do aluno: ");
                    idade = Convert.ToInt32(Console.ReadLine());
                    soma += idade;
                }
                if ((float)soma / n < 26)
                    Console.WriteLine("A turma é jovem");
                else
                    if ((float)soma / n <= 60)
                    Console.WriteLine("A turma é adulta");
                else
                    Console.WriteLine("A turma é idosa");
            }
            else
                Console.WriteLine("A turma não tem alunos");
        }
        public static void ex021()
        {
            double A = 80000, tcA = 0.03, B = 200000, tcB = 0.015, a = 0;
            while (A < B)
            {
                A *= (1 + tcA);
                B *= (1 + tcB);
                a++;
            }
            Console.WriteLine("A população do país A igualará ou ultrapassará a população do país B em {0} anos", a);
        }
        public static void ex022()
        {
            double A, B, a, tcA, tcB;
            char c;
            do
            {
                a = 0;
                Console.Write("Qual a população do país A: ");
                A = Convert.ToDouble(Console.ReadLine());
                do
                {
                    Console.Write("Qual a taxa de crescimento da população do país A: ");
                    tcA = Convert.ToDouble(Console.ReadLine());
                } while (tcA < 0 || tcA > 1);//Validação da taxa de crescimento para valores entre 0 e 1
                Console.Write("Qual a população do país B ( B>A): ");
                B = Convert.ToDouble(Console.ReadLine());
                do
                {
                    Console.Write("Qual a taxa de crescimento da população do país B: ");
                    tcB = Convert.ToDouble(Console.ReadLine());
                } while (tcB < 0 || tcB > 1);//Validação da taxa de crescimento para valores entre 0 e 1
                if (A >= B && tcB <= tcA)
                    Console.WriteLine("A população do país A nunca igualará ou ultrapassará a população do país B");
                else
                {
                    while (A < B)
                    {
                        A *= (1 + tcA);
                        B *= (1 + tcB);
                        a++;
                    }
                    Console.WriteLine("A população do país A igualará ou ultrapassará a população do país B em {0} anos", a);
                }
                Console.Write("Pretende fazer outra simulação (s/n): ");
                c = Convert.ToChar(Console.ReadLine());
            } while (c == 's');
        }
        public static void ex023()
        {
            int n, num, soma = 0;
            Console.Write("Quantas turmas? ");
            n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                do
                {
                    Console.Write("Quantos alunos tem a turma {0}? ", i);
                    num = Convert.ToInt32(Console.ReadLine());
                } while (num < 1 || num > 30);// o nº de alunos por turma deve estar entre 1 e 30
                soma += num;
            }
            Console.WriteLine("Média de alunos por turma: {0}", soma / (float)n); // O type cast (float)n garante que a divisão é real

        }
        public static void ex024()
        {
            int j, n, soma = 1; // A soma é inicializada em 1 porque o teste dos divisores começa em 2 já que 1 é divisor de todos os nºs
            Console.Write("Qual o número? ");
            n = Convert.ToInt32(Console.ReadLine());
            for (j = 2; j <= (Math.Sqrt((float)n)); j++)
                if (n % j == 0)
                    if (j == n / j)
                        soma += j;
                    else
                        soma += j + n / j;
            if (soma == n)
                Console.WriteLine("{0} é um número perfeito", n);
            else
                Console.WriteLine("{0} não é um número perfeito", n);
        }
        public static void ex025()
        {
            int n, soma = 0, i = 0;
            Console.Write("Introduza um número decimal? ");
            n = Convert.ToInt32(Console.ReadLine());
            do
            {
                soma += n % 2 * (int)Math.Pow(10, i++);
                n = n / 2;
            } while (n > 0);
            Console.WriteLine("Binário: {0}", soma);
        }
        public static void ex026()
        {
            int n, soma = 0, i = 0;
            Console.Write("Introduza um número binário? "); // Não é feita validação, ou seja, o utilizador pode introduzir caracteres inválidos e gerar excepções (ERROS)
            n = Convert.ToInt32(Console.ReadLine());
            do
            {
                soma += n % 10 * (int)Math.Pow(2, i++);
                n = n / 10;
            } while (n > 0);
            Console.WriteLine("Decimal: {0}", soma);
        }
        public static void ex027()
        {
            int n, soma = 0;
            Console.Write("Quantas entradas para menores de 4 anos? ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Quantas entradas para crianças entre os 6 e os 12 anos? ");
            n = Convert.ToInt32(Console.ReadLine());
            soma += 6 * n;
            Console.Write("Quantas entradas para crianças entre os 12 e os 17 anos? ");
            n = Convert.ToInt32(Console.ReadLine());
            soma += 12 * n;
            Console.Write("Quantas entradas para adultos? ");
            n = Convert.ToInt32(Console.ReadLine());
            soma += 18 * n;
            Console.WriteLine("Total: {0}", soma);
        }

        public static void ex28()
        {
            int num = 0, r, d, n;
            do
            {
                n = ++num;
                d = n % 10;
                n = n / 10;
                r = n;
                while (n != 0)
                {
                    n = n / 10;
                    d *= 10;
                }
            } while (5 * num != d + r);
            Console.WriteLine("{0}", num);
        }
        public static void ex29()
        {
            int num = 0, i = 0, soma = 0, ant = 0;
            string romano;
            Console.Write("Introduza um número romano: ");// A correção do número romano não é validada
            romano = Console.ReadLine();
            for (i = 0; i < romano.Length; i++)
            {
                switch (romano[i])
                {
                    case 'M': num = 1000; break;
                    case 'D': num = 500; break;
                    case 'C': num = 100; break;
                    case 'L': num = 50; break;
                    case 'X': num = 10; break;
                    case 'V': num = 5; break;
                    case 'I': num = 1; break;
                    default:
                        num = 0;
                        Console.WriteLine("Número inválido");
                        return; //O programa termina a execução neste ponto, em alternativa poderia ter usado goto
                }
                if (ant < num)
                    soma += num - 2 * ant;
                else
                    soma += num;
                ant = num;
            }
            Console.WriteLine("Árabe: {0}", soma);
        }
        public static void ex30()
        {
            int peso, linha, coluna, num;
            string[,] romano = new string[,] {{"I","II","III","IV","V","VI","VII","VIII","IX"},
                                             {"X","XX","XXX","XL","L","LX","LXX","LXXX","XC"},
                                             {"C","CC","CCC","CD","D","DC","DCC","DCCC","CM"},
                                             {"M","MM","MMM","","","","","",""}};
            string result = "";
            Console.Write("Introduza um número árabe até 3999: ");
            num = Convert.ToInt32(Console.ReadLine());
            for (peso = 1000, linha = 3; peso >= 1; peso = peso / 10, linha--)
            {
                coluna = num / peso;
                if (coluna >= 1)
                {
                    num = num % peso;
                    result += romano[linha, coluna - 1];
                }
            }
            Console.WriteLine("Romano: {0}", result);
        }
        public static void ex31()
        {
            int j, n, soma;
            for (n = 2; n < 10000; n++)
            {
                soma = 1;
                for (j = 2; j <= (Math.Sqrt((float)n)); j++)
                    if (n % j == 0)
                        if (j == n / j)
                            soma += j;
                        else
                            soma += j + n / j;
                if (soma == n)
                    Console.WriteLine(n);
            }

        }
        public static void ex032()
        {
            ulong j, m = 2, n, k = 0;
            bool primo = true;
            for (n = 2; n < 62; n++)
            {
                m = m * 2 - 1;
                primo = true;
                j = 1;
                while (++j <= (Math.Sqrt(m)) && primo)
                    if (m % j == 0)
                        primo = false;
                if (primo)
                    Console.WriteLine(m);
                k += j - 2;
                m++;
            }
        }
        public static void ex33()
        {
            long i = 1, num = 182, n, r;
            do
            {
                n = num * ++i;
                do
                {
                    r = n % 10;
                    n /= 10;
                } while (n != 0 && (r == 4));
            }
            while (n != 0 || r != 4);
            Console.WriteLine(num * i);
        }
        public static void ex034()
        {
            long i = 1, num = 416, n, r;
            do
            {
                n = num * ++i;
                do
                {
                    r = n % 10;
                    n /= 10;
                } while (n != 0 && (r == 1 || r == 2));
            }
            while (n != 0 || r != 1 && r != 2);
            Console.WriteLine(num * i);
        }

    }

}

