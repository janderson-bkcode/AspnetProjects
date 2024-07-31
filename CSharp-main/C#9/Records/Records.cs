using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace consoleApp;

public class Program{

    public static void Main(string[] args){
        // Cliente cliente = new Cliente{
        //     Nome = "Janderson",
        //     Sexo = "Masculino"
        // };

        // // Criando cópias 

        // var novocliente  = cliente with {Nome="Andreia"};

        // Console.WriteLine(novocliente);

        // // os tipos records são tipos de referência e não tipos de valor 
        // // Isso significa que você pode comparar dois Recods por seus valores de propriedade para uma igualdade como mostrado a seguir:

        // System.Console.WriteLine(novocliente == cliente);// false

        // var outroCliente = novocliente with {Nome="Janderson"};

        // System.Console.WriteLine(outroCliente == cliente);//true

        //Trabalhando com Construtores e destrutores

        // construção 

        // var cliente2 = new Cliente("Janderson","Masculino");

        // // desconstrução 

        // var (nome,sexo) = cliente2;

        // System.Console.WriteLine(nome);
        // System.Console.WriteLine(sexo);


        // Chamando o construtor Positional Record

        var cliente3 = new Cliente2("Naruto","Masculino");

        var novoCliente3 = cliente3 with {Name ="Sasuke"};

        System.Console.WriteLine(novoCliente3);


        // herança

        var pessoa = new Pessoa{id = 1};

        var novaPessoa = pessoa with{id = 2};

        var pessoaAluno = new Aluno{id = 3 , CadastroUnico="cadastro"};

        var novaPessoaAlunoAluno = pessoaAluno with{CadastroUnico = "cadastro2"};


        // Aqui criando uma variavel do tipo Pessoa que recebe um objeto Aluno, ou seja, um novo objeto PEssoa
        Pessoa pessoa3 = new Aluno{id =4,CadastroUnico = "cadastroX"};

        var novaPessoa4 = pessoa3 with{id = 5};

    }
}



public record Cliente{

    public string Nome  { get;init;}
    public string Sexo { get; init; }    

    public Cliente(string nome, string sexo)
    {
        Nome = nome;
        Sexo = sexo;
    }

    public void Deconstruct(out string nome, out string sexo){
        nome = Nome;
        sexo = Sexo;
    }
} 


/*
Um tipo Record de Cliente;
Com as propriedades públicas init-only Nome, Sobrenome e Email;
Com um construtor público parametrizado;
Com um método de desconstrução;
*/
public record Cliente2(string Name,string Sexo);


// Herança 

public record Pessoa{
    public int id { get; init; }
}

public record Aluno : Pessoa{
    public string CadastroUnico { get; set; }
}

