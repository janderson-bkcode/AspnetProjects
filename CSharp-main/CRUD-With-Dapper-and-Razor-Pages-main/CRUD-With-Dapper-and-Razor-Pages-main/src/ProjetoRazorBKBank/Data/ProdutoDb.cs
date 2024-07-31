using Dapper;
using ProjetoRazorBKBank.Interfaces.Db;
using ProjetoRazorBKBank.Models.DTOs;
using ProjetoRazorBKBank.Models.Responses;

namespace ProjetoRazorBKBank.Data
{
    public class ProdutoRepository : DbSession, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<BaseResponse> CreateAsync(ProdutoDTO obj)
        {
            string sql = @"
                            INSERT INTO Produto (Descricao, Preco, Estoque)
                            VALUES (@Descricao, @Preco, @Estoque)
                            ";
            return await ExecuteAsync(() =>
                connection.ExecuteAsync(
                    sql,
                    param: new
                    {
                        Descricao = obj.Descricao,
                        Preco = obj.Preco,
                        Estoque = obj.Estoque
                    }
                ));
        }

        public async Task<GenericResponse<ProdutoDTO>> ReadByIdAsync(int id)
        {
            string sql = @"SELECT * FROM Produto WHERE Id = @id";

            GenericResponse<IEnumerable<ProdutoDTO>> queryReponse = await QueryAsync<IEnumerable<ProdutoDTO>>
                (() => connection.QueryAsync<ProdutoDTO>(sql, new { Id = id }));

            return new GenericResponse<ProdutoDTO>
            {
                ErrorCode = queryReponse.ErrorCode,
                Message = queryReponse.Message,
                Response = queryReponse.Response.FirstOrDefault()
            };
        }

        public async Task<BaseResponse> UpdateAsync(ProdutoDTO obj)
        {
            string sql = @"
                            UPDATE 
                                Produto
                            SET 
                                Descricao = @Descricao,
                                Preco = @Preco,
                                Estoque = @Estoque
                            WHERE
                                Id = @Id
                                ";
            return await ExecuteAsync(() => connection.ExecuteAsync
            (
                sql,
                new
                {
                    Descricao = obj.Descricao,
                    Preco = obj.Preco,
                    Estoque = obj.Estoque,
                    Id = obj.Id
                })
            );
        }

        public async Task<BaseResponse> DeleteByIdAsync(int id)
        {
            string sql = @"
                            DELETE FROM
                                Produto
                            WHERE
                                Id = @Id
                            ";
            return await ExecuteAsync(() => connection.ExecuteAsync(sql, new { Id = id }));
        }

        public async Task<GenericResponse<IEnumerable<ProdutoDTO>>> GetAllAsync()
        {
            string sql = "SELECT * FROM Produto";
            return await QueryAsync<IEnumerable<ProdutoDTO>>(() => connection.QueryAsync<ProdutoDTO>(sql));
        }
    }
}