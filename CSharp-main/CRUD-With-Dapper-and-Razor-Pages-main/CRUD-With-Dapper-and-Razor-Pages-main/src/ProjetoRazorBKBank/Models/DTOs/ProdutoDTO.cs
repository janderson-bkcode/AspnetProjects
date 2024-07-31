using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoRazorBKBank.Models.DTOs;

public class ProdutoDTO
{
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo descrição é obrigatório"), DisplayName("Descrição"), StringLength(maximumLength:300, MinimumLength = 10)]
    public string Descricao { get; set; }
    [Required, DataType(DataType.Currency), DisplayName("Preço do produto")]
    public decimal Preco { get; set; }
    [Required, DisplayName("Quantidade em estoque")]
    public int Estoque { get; set; }
}