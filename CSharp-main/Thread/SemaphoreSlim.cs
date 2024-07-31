using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace SeuNamespace
{
    public class SeuController : ControllerBase
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(20); // Limite de 20 tarefas simultâneas

        [HttpGet]
        [Route("Executar3000")]
        public async Task<IActionResult> Executar3000()
        {
            try
            {
                int totalPosts = 3000;

                List<Task> tasks = new List<Task>();

                for (int i = 0; i < totalPosts; i++)
                {
                    await semaphore.WaitAsync(); // Aguarda um slot disponível para executar a tarefa

                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            PixPaymentRequest pixPaymentRequest = new PixPaymentRequest'
                            {
                                // Configurações do PixPaymentRequest
                            };

                            var options = new RestClientOptions("BaseUrl")
                            {
                                MaxTimeout = -1,
                            };

                            var client2 = new RestClient(options);
                            var request2 = new RestRequest("/api/metodo", Method.Post);
                            request2.AddHeader("Content-Type", "application/json");
                            request2.AddJsonBody(pixPaymentRequest);

                            RestResponse response2 = await client2.ExecuteAsync(request2);

                            Console.WriteLine(response2.Content);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error sending : " + ex.Message);
                        }
                        finally
                        {
                            semaphore.Release(); // Libera o slot ocupado pela tarefa
                        }
                    }));
                }

                await Task.WhenAll(tasks); // Aguarda todas as tarefas serem concluídas

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }

    // Restante das classes
}
