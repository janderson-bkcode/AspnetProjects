using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository2<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);
}


public class Repository<T> : IRepository<T> where T : class
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

    public async Task<T> InsertAsync(T entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var properties = GetNonIdentityProperties(entity);
            var propertyNames = string.Join(", ", properties.Select(p => p.Name));
            var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
            var query = $"INSERT INTO {typeof(T).Name} ({propertyNames}) VALUES ({parameterNames}); SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var parameters = GetDynamicParameters(entity, properties);
            var id = await connection.QueryFirstOrDefaultAsync<int>(query, parameters);
            var insertedEntity = await GetByIdAsync(id);
            return insertedEntity;
        }
    }

    public async Task<T> UpdateAsync(T entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var properties = GetNonIdentityProperties(entity);
            var propertySetters = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            var query = $"UPDATE {typeof(T).Name} SET {propertySetters} WHERE Id = @Id";
            var parameters = GetDynamicParameters(entity, properties);
            parameters.Add("@Id", GetIdPropertyValue(entity));
            var result = await connection.ExecuteAsync(query, parameters);
            var updatedEntity = await GetByIdAsync(GetIdPropertyValue(entity));
            return updatedEntity;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var query = "DELETE FROM " + typeof(T).Name + " WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            var result = await connection.ExecuteAsync(query, parameters);
            return result == 1;
        }
    }

    private IEnumerable<PropertyInfo> GetNonIdentityProperties(T entity)
    {
        var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
        return properties;
    }

    private DynamicParameters GetDynamicParameters(T entity, IEnumerable<PropertyInfo> properties)
    {
        var parameters = new DynamicParameters();
        foreach (var property in properties)
        {
            var value = property.GetValue(entity);
            parameters.Add("@" + property.Name, value);
        }
        return parameters;
    }

   private int GetIdPropertyValue(T entity)
{
    var idProperty = typeof(T).GetProperty("Id");
    var idValue = (int)idProperty.GetValue(entity);
    return idValue;
}
}