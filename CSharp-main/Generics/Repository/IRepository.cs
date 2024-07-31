using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);
}
public class Repository<T> : IRepository<T>
{
    private readonly string _connectionString;

    public Repository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "SELECT * FROM " + typeof(T).Name;
            var result = await connection.QueryAsync<T>(query);
            return result.ToList();
        }
    }

    public async Task<T> GetByIdAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "SELECT * FROM " + typeof(T).Name + " WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
            return result;
        }
    }

    public async Task<int> AddAsync(T entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "INSERT INTO " + typeof(T).Name + " VALUES (@Property1, @Property2, ...)";
            var parameters = GetDynamicParameters(entity);
            var result = await connection.ExecuteAsync(query, parameters);
            return result;
        }
    }

    public async Task<int> UpdateAsync(T entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "UPDATE " + typeof(T).Name + " SET Property1 = @Property1, Property2 = @Property2, ... WHERE Id = @Id";
            var parameters = GetDynamicParameters(entity);
            parameters.Add("@Id", GetIdPropertyValue(entity));
            var result = await connection.ExecuteAsync(query, parameters);
            return result;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "DELETE FROM " + typeof(T).Name + " WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await connection.ExecuteAsync(query, parameters);
            return result;
        }
    }

    private DynamicParameters GetDynamicParameters(T entity)
    {
        var properties = typeof(T).GetProperties();
        var parameters = new DynamicParameters();
        foreach (var property in properties)
        {
            parameters.Add("@" + property.Name, property.GetValue(entity));
        }
        return parameters;
    }

    private int GetIdPropertyValue(T entity)
    {
        var properties = typeof(T).GetProperties();
        var idProperty = properties.FirstOrDefault(p => p.Name == "Id");
        if (idProperty == null)
        {
            throw new KeyNotFoundException("The entity does not have an 'Id' property.");
        }
        return (int)idProperty.GetValue(entity);
    }
}

