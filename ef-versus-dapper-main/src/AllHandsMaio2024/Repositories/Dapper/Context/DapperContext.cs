using Microsoft.Data.SqlClient;
using System.Data;

namespace AllHandsMaio2024.Repositories.Dapper.Context;

public interface IDapperContext
{
    public IDbConnection GetConnection();
}

public class DapperContext : IDapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection GetConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("default"));
    }
}
