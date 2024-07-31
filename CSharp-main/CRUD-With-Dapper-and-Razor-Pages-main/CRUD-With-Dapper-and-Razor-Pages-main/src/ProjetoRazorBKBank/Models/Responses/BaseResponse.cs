using ProjetoRazorBKBank.Models.Enums;

namespace ProjetoRazorBKBank.Models.Responses
{
    public class BaseResponse
    {
        public ErrorCode ErrorCode { get; set; }
        public string Message { get; set; }
    }
}