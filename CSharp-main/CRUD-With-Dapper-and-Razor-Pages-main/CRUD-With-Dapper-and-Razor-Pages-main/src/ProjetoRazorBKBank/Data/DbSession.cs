using System.Data;
using MySql.Data.MySqlClient;
using ProjetoRazorBKBank.Models.Enums;
using ProjetoRazorBKBank.Models.Responses;

namespace ProjetoRazorBKBank.Data
{
    public abstract class DbSession
    {
        protected readonly IDbConnection connection;

        public DbSession(IConfiguration configuration)
        {
            connection = new MySqlConnection(configuration.GetConnectionString("Personal"));
        }

        public async Task<GenericResponse<TResult>> QueryAsync<TResult> (Func<Task<TResult>> func)
        {
            var response = new GenericResponse<TResult>();
            try
            {
                connection.Open(); 
                TResult queryResponse = await func();

                if(queryResponse != null)
                {
                    response.ErrorCode = ErrorCode.Sucess;
                    response.Message = ErrorCode.Sucess.ToString(); 
                    response.Response = queryResponse;
                    
                }
                else
                {
                    response.ErrorCode = ErrorCode.DataBaseNoData;
                    response.Message = ErrorCode.DataBaseNoData.ToString();
                }
            }
            catch(Exception ex)
            {
                response.ErrorCode = ErrorCode.DataBaseException;
                response.Message = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }
        public async Task<BaseResponse> ExecuteAsync (Func<Task<int>> func)
        {
            var response = new BaseResponse();
            try
            {
                connection.Open(); 
                int rowsAffected = await func();

                if (rowsAffected == 1)
                {
                    response.ErrorCode = 0;
                    response.Message = ErrorCode.Sucess.ToString(); 
                }
                else
                {
                    response.ErrorCode = ErrorCode.DataBaseError;
                    response.Message = ErrorCode.DataBaseError.ToString();
                }
            }
            catch(Exception ex)
            {
                response.ErrorCode = ErrorCode.DataBaseException;
                response.Message = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }
        
    }
}
