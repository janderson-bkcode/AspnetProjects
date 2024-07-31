using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRazorBKBank.Models.Responses
{
    public class GenericResponse<T> : BaseResponse
    {
        public T Response { get; set; }
    }
}