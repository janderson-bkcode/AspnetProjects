using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTesteFormUploadFileAndPersist.Data;
using WebApplicationTesteFormUploadFileAndPersist.Models;

namespace WebApplicationTesteFormUploadFileAndPersist.Controllers
{
    public class ExcelController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReceberArquivo(IFormFile arquivo)
        {
            if (arquivo != null && arquivo.Length > 0)
            {
                var dados = new List<DadosExcel>();

                using (var workbook = new XLWorkbook(arquivo.OpenReadStream()))
                {
                    var worksheet = workbook.Worksheet(1); // Usa a primeira planilha de trabalho
                    var range = worksheet.RangeUsed(); // verifica os ranges que estão sendo usados(d5:d6) por exemplo

                    for (int row = 7; row <= (range.RowCount() - 2); row++)// Eliminando a leitura das duas utimas linhas que são o total
                    {
                        var dado = LerExcelPopularClasse(range, row);
                        dados.Add(dado);
                    }
                }

                var query = from excel in dados
                            where excel.TipoMovimento.Equals(1072) // PIX RECEBIDO
                            select excel;

                //Comparar com lista de BD que possui mesmo EndtoEnd e OperationType na mesma data

                var query2 = from excel2 in dados
                             where excel2.TipoMovimento.Equals(1054)
                             select excel2;

                foreach (var item in query)
                {
                    Console.WriteLine($"Credito {item.EndToEnd}");
                }
                foreach (var item in query2)
                {
                    Console.WriteLine($"Débito {item.EndToEnd}");
                }

                using (var contexto = new MeuDbContext())
                {
                    contexto.PixTransactionConciliation.AddRange(dados);
                    contexto.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View();
        }

        private DadosExcel LerExcelPopularClasse(IXLRange range, int row)
        {
            DateTime Data = Convert.ToDateTime(range.Cell(row, 1).Value.ToString());
            string Origem = range.Cell(row, 2).Value.ToString();
            string Cnpj = range.Cell(row, 3).Value.ToString();
            string Nome = range.Cell(row, 4).Value.ToString();
            string TipoMovimento = range.Cell(row, 5).Value.ToString();
            decimal Valor = Convert.ToDecimal(range.Cell(row, 6).Value.ToString());
            string EndToEnd = range.Cell(row, 7).Value.ToString();
            string Condicao = range.Cell(row, 8).Value.ToString();
            string DescCondicao = range.Cell(row, 9).Value.ToString();
            string NumeroRemessa = range.Cell(row, 10).Value.ToString();
            string AgenciaOrigem = range.Cell(row, 11).Value.ToString();
            string AgenciaDestino = range.Cell(row, 12).Value.ToString();
            var dado = new DadosExcel(Data.Date, Origem, Cnpj, Nome, TipoMovimento, Valor, EndToEnd, Condicao, DescCondicao, NumeroRemessa, AgenciaOrigem, AgenciaDestino);

            return dado;
        }
    }
}