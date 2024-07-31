using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoRazorBKBank.Interfaces.Db;
using ProjetoRazorBKBank.Models.DTOs;
using ProjetoRazorBKBank.Models.Enums;

namespace ProjetoRazorBKBank.Pages.Produtos
{
    public class Index : PageModel
    {
        public IReadOnlyCollection<ProdutoDTO> Produtos { get; set; }
        private readonly IProdutoRepository _produtoRepository;
        public Index(IProdutoRepository produtoHandler)
        {
            _produtoRepository = produtoHandler;
        }
        
        public IActionResult OnGet()
        {
            var repositoryResponse = _produtoRepository.GetAllAsync().Result;

            if(repositoryResponse.ErrorCode != ErrorCode.Sucess)
                return RedirectToPage("../Error", new { message = repositoryResponse.Message });
            
            Produtos = repositoryResponse.Response.ToList();

            return Page();
        }
    }
}