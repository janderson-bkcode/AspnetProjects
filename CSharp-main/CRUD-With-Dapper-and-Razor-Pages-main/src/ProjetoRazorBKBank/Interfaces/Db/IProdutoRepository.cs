using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoRazorBKBank.Models.DTOs;
using ProjetoRazorBKBank.Models.Responses;

namespace ProjetoRazorBKBank.Interfaces.Db
{
    public interface IProdutoRepository : ICrud<ProdutoDTO>
    {
        Task<GenericResponse<IEnumerable<ProdutoDTO>>> GetAllAsync();
    }
}