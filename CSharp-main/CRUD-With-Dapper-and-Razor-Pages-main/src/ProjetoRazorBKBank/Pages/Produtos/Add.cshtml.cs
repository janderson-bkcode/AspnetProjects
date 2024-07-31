using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoRazorBKBank.Interfaces.Db;
using ProjetoRazorBKBank.Models.DTOs;
using ProjetoRazorBKBank.Models.Enums;

namespace ProjetoRazorBKBank.Pages.Produtos;

public class Add : PageModel
{
    private readonly IProdutoRepository _produtoRepository;

    public Add(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    [BindProperty]
    public ProdutoDTO Produto { get; set; }
    public IActionResult OnGet()
    {
        Produto = new ProdutoDTO();

        return Page();
    }

    public IActionResult OnPost()
    {
        var repositoryResponse = _produtoRepository.CreateAsync(Produto).Result;
        
        if(repositoryResponse.ErrorCode != ErrorCode.Sucess)
            return RedirectToPage("../Error", new { message = repositoryResponse.Message });

        return RedirectToPage("./Index");
    }
}