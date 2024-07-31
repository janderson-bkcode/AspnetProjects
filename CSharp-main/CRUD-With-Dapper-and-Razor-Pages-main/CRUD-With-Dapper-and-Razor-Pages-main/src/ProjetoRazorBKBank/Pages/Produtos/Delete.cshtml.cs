using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoRazorBKBank.Interfaces.Db;
using ProjetoRazorBKBank.Models.Enums;

namespace ProjetoRazorBKBank.Pages.Produtos;

public class Delete : PageModel
{
    private readonly IProdutoRepository _produtoRepository;

    public Delete(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public IActionResult OnGet(int id)
    {
        var repositoryResponse = _produtoRepository.DeleteByIdAsync(id).Result;
        if (repositoryResponse.ErrorCode != ErrorCode.Sucess)
            return RedirectToPage("../Error", new { message = repositoryResponse.Message });

        return Page();
    }
}