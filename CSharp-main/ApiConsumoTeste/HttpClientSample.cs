using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiConsumoTeste
{

    public class Produto
    {
        public string id { get; set; }

        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public string Categoria { get; set; }

    }

    public static class Program
    {
        static HttpClient client = new HttpClient();

        static void MostrarProduto(Produto produto)
        {
            Console.WriteLine($"Nome:{produto.Nome}\tPreco:{produto.Preco}\t Categoria:{produto.Categoria}");
        }

        static async Task<Uri> CriarProdutoAsync(Produto produto)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("api/products", produto).Result;

            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Produto> GetProdutoAsync(string path)
        {
            Produto produto = null;
            
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                produto = await response.Content.ReadAsAsync<Produto>();
            }
            return produto; 
        }

        static async Task<Produto> UpdateProdutoAsync(Produto produto)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/produto/{produto.id}",produto);
            response.EnsureSuccessStatusCode();

            //Deserializando o produto atualizado da response body

            produto = await response.Content.ReadAsAsync<Produto>();
            return produto;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }


       public static async Task RunAsync()
        {
            // Update port # in the following line.

            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                //Cria produto 

                Produto produto = new Produto
                {
                    id = "001",
                    Nome = "Controle",
                    Preco = 100,
                    Categoria = "Game"
                };

                var url = await CriarProdutoAsync(produto);//.Result;
                Console.WriteLine($"Produto Criado na {url}");

                //Pegar Produtos

                produto = await GetProdutoAsync(url.PathAndQuery);
                MostrarProduto(produto);

                //Atualizar Produto 

                Console.WriteLine("Atualizando preco");
                produto.Preco = 80;
                await UpdateProdutoAsync(produto);

                //Obter produto atualizado 

                Console.WriteLine("Produto atualizado");
                produto = await GetProdutoAsync(url.PathAndQuery);
                MostrarProduto(produto);

                //Deletar produto 

                var statusCode = await DeleteProductAsync(produto.id);
                Console.WriteLine($"Deletado(HTTP STATUS ={(int)statusCode}");



            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }

        public static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }
    }
}
