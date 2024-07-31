using System;
using System.Reflection;
using System;

namespace AssemblyTeste
{
    class Program
    {
        public static void Main(string[] args)
        {
            Assembly assembly = typeof(Program).Assembly;
            System.Console.WriteLine("Nome do Assembly");
            System.Console.WriteLine(assembly.FullName);

            AssemblyName assemblyName = assembly.GetName();
            System.Console.WriteLine("\nNome:{0}",assemblyName.Name);
            System.Console.WriteLine("\nVersion:{0}{1}",assemblyName.Version.Major,assemblyName.Version.Minor);

            System.Console.WriteLine("\nCode base");
            System.Console.WriteLine(assembly.CodeBase);

            System.Console.WriteLine("\nAssembly EntryPoint");
            System.Console.WriteLine(assembly.EntryPoint);

            System.Console.WriteLine();
            var dataCriacao = File.GetCreationTime(assembly.Location);
            System.Console.WriteLine($"Data Criação {dataCriacao}");

            System.Console.WriteLine(FormatEmbossingName("Janderson Barbosa"));
            System.Console.WriteLine(FormatEmbossingName2("Janderson"));  System.Console.WriteLine(FormatEmbossingName2("Janderson Barbosa Gonçalves"));
        }
           public static  string FormatEmbossingName(string FullName)
        {
            var nameParts = FullName.Trim().Split(' ');

            var embossingName = nameParts[0];

            for (int i = 1; i < nameParts.Length - 1; i++)
            {
                if (nameParts[i].Length > 2)
                {
                    embossingName += " " + nameParts[i].Substring(0, 1);
                }
            }

            embossingName += " " + nameParts[nameParts.Length - 1];

            if (embossingName.Length > 20)
            {
                embossingName = $"{nameParts[0]} {nameParts[nameParts.Length - 1]}";

                if(embossingName.Length > 20)
                {
                    embossingName = embossingName.Substring(0, 20);
                }                                
            }

            return embossingName.ToUpper();
        }
  public static string FormatEmbossingName2(string FullName)
        {
            var nameParts = FullName.Trim().Split(' ');

            if (nameParts.Length == 1)
            {
                return FullName.Trim().ToUpper();
            }

            if (nameParts.Length > 3)
            {
                return $"{nameParts[0]} {nameParts[1][0]} {nameParts[nameParts.Length - 1]}".Trim().ToUpper();
            }

            var embossingName = nameParts[0];

            for (int i = 1; i < nameParts.Length - 1; i++)
            {
                if (nameParts[i].Length > 2)
                {
                    embossingName += " " + nameParts[i].Substring(0, 1);
                }
            }

            embossingName += " " + nameParts[nameParts.Length - 1];

            if (embossingName.Length > 20)
            {
                embossingName = $"{nameParts[0]} {nameParts[nameParts.Length - 1]}";

                if (embossingName.Length > 20)
                {
                    embossingName = embossingName.Substring(0, 20);
                }
            }

            return embossingName.Trim().ToUpper();
        }


    }
    
}