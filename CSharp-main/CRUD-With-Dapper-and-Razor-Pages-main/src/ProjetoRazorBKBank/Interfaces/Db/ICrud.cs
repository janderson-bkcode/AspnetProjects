using ProjetoRazorBKBank.Models.Responses;

namespace ProjetoRazorBKBank.Interfaces.Db
{
    public interface ICrud<T>
    {
        public Task<BaseResponse> CreateAsync(T obj);
        public Task<GenericResponse<T>> ReadByIdAsync(int id);
        public Task<BaseResponse> UpdateAsync(T obj);
        public Task<BaseResponse> DeleteByIdAsync(int id);
    }
}
