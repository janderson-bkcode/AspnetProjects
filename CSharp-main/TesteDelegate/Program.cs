
namespace TesteDelegate
{
    //Example 1
    delegate List<int> MeuDelegate(int x, int y);

    // Example 2
    public delegate int del(int x);
    public delegate void Deltexto(string texto);

    //Example 3
    public delegate int DelegateCalc(int num1, int num2);

    public class Program
    {
        public static void Main(string[] args)
        {
            //Example 1
            MeuDelegate meuDelegate = new MeuDelegate(Soma);
            meuDelegate += Subtracao;
            meuDelegate += Multiplicacao;

            List<int> resultados = meuDelegate(2, 3);

            foreach (int resultado in resultados)
            {
                Console.WriteLine($"Resultado => {resultado}");
            }

            List<Delegate> delegates = meuDelegate.GetInvocationList().ToList();

            foreach (Delegate del in delegates)
            {
                MeuDelegate metodo = (MeuDelegate)del;
                List<int> resultadoMetodo = metodo(2, 3);
                Console.WriteLine(resultadoMetodo[0]);
            }

            // Example 2
            del handle = testeDel;
            handle += testeDel2;
            handle += (a) => 2 * 10;
            Console.WriteLine(handle(3));
            Deltexto handleTexto = textoTeste;
            MetodoCallback(1, 2, handleTexto);

            System.Console.WriteLine("///App");
            ChatApp chatApp = new ChatApp();

            Usuario usuario1 = new Usuario("Alice");
            Usuario usuario2 = new Usuario("Bob");

            // Registrar os callbacks dos usuários para receber mensagens
            chatApp.NovaMensagem += usuario1.ReceberMensagem;
            chatApp.NovaMensagem += usuario2.ReceberMensagem;

            chatApp.EnviarMensagem("Carol", "Olá, pessoal!");
            chatApp.EnviarMensagem("janderson", "Olá, pessoal!");

            //Example3
            //
            // Calculadora calc = new Calculadora();
            // DelegateCalc delegateTeste = new DelegateCalc(calc.Add(1, 2));
        }

        // Example1
        static List<int> Soma(int x, int y)
        {
            List<int> resultados = new List<int>();
            int resultado = x + y;
            resultados.Add(resultado);
            return resultados;
        }

        static List<int> Subtracao(int x, int y)
        {
            List<int> resultados = new List<int>();
            int resultado = x - y;
            resultados.Add(resultado);
            return resultados;
        }

        static List<int> Multiplicacao(int x, int y)
        {
            List<int> resultados = new List<int>();
            int resultado = x * y;
            resultados.Add(resultado);
            return resultados;
        }
        public static int testeDel(int num)
        {
            return num * 2;
        }

        // Example2
        public static int testeDel2(int num)
        {
            return num * 3;
        }
        public static void textoTeste(string message)
        {
            Console.Write(message);
        }
        public static void MetodoCallback(int num, int num2, Deltexto callback)
        {
            callback("O calculo é" + (num + num2).ToString());
        }
    }
}

// Delegate para notificar os usuários sobre uma nova mensagem
delegate void NovaMensagemDelegate(string remetente, string mensagem);

class ChatApp
{
    public event NovaMensagemDelegate NovaMensagem;

    public void EnviarMensagem(string remetente, string mensagem)
    {
        // Lógica para enviar a mensagem

        // Disparar o evento de nova mensagem
        if (NovaMensagem != null)
        {
            NovaMensagem(remetente, mensagem);
        }
    }
}

class Usuario
{
    public string Nome { get; }

    public Usuario(string nome)
    {
        Nome = nome;
    }

    public void ReceberMensagem(string remetente, string mensagem)
    {
        Console.WriteLine("Nova mensagem de {0}: {1}", remetente, mensagem);
    }
}

// Example3

public class Calculadora
{
    public int Add(int num, int num2)
    {
        return num + num2;
    }

    public int Multiply(int num, int num2)
    {
        return num * num2;
    }
}