using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoRazorBKBank.Interfaces.Db;
using ProjetoRazorBKBank.Models.DTOs;
using ProjetoRazorBKBank.Models.Enums;

namespace ProjetoRazorBKBank.Pages.Produtos
{
    public class Edit : PageModel
    {
        [BindProperty]
        public ProdutoDTO Produto { get; set; }
        private readonly IProdutoRepository _produtoRepository;

        public Edit(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public IActionResult OnGet(int id)
        {
            var repositoryResponse = _produtoRepository.ReadByIdAsync(id).Result; 
            
            if(repositoryResponse.ErrorCode != ErrorCode.Sucess)
                return RedirectToPage("../Error", new { message = repositoryResponse.Message });
            
            Produto = repositoryResponse.Response;
            return Page(); 
        }

        public IActionResult OnPost()
        {
            var repositoryResponse = _produtoRepository.UpdateAsync(Produto).Result;

            if (repositoryResponse.ErrorCode != ErrorCode.Sucess)
                return RedirectToPage("../Error", new { message = repositoryResponse.Message });

            return RedirectToPage("./Index");
        }
    }
}