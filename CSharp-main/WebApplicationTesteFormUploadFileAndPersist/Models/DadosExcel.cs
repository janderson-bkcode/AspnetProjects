using System.ComponentModel.DataAnnotations;

namespace WebApplicationTesteFormUploadFileAndPersist.Models
{
    public class DadosExcel
    {
        public DadosExcel(DateTime? data, string? origem, string? cnpj, string? nome, string? tipoMovimento, decimal? valor, string? endToEnd, 
            string? condicao, string? descCondicao, string? numeroRemessa, string? agenciaOrigem, string? agenciaDestino)
        {
            Data = data;
            Origem = origem;
            Cnpj = cnpj;
            Nome = nome;
            TipoMovimento = ReturnOperationType(tipoMovimento);
            Valor = valor;
            EndToEnd = endToEnd;
            Condicao = condicao;
            DescCondicao = descCondicao;
            NumeroRemessa = numeroRemessa;
            AgenciaOrigem = agenciaOrigem;
            AgenciaDestino = agenciaDestino;
        }
        protected DadosExcel()
        {

        }

        [Key]
        public int Id { get; set; }
        public DateTime? Data { get; set; }
        public string? Origem { get; set; }
        public string? Cnpj { get; set; }
        public string? Nome { get; set; }
        public int? TipoMovimento { get; set; }
        public decimal? Valor { get; set; }
        public string? EndToEnd { get; set; }
        public string? Condicao { get; set; }
        public string? DescCondicao { get; set; }
        public string? NumeroRemessa { get; set; }
        public string? AgenciaOrigem { get; set; }
        public string? AgenciaDestino { get; set; }

        public override string ToString()
        {
            return $"Data : {Data} Origem: {Origem} Cnpj=> {Cnpj} Nome => {Nome}{TipoMovimento}";
        }



        public int ReturnOperationType(string tipoMovimento)
        {
            switch (tipoMovimento)
            {
                case "CREDITO":
                    return 1072;
                case "DEBITO":
                    return 1054;
                default:
                    break;
            }
            return 0;
        }
    }                
}