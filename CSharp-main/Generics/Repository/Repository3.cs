using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generics.Repository
{
    public interface Repository3
    {
        
    }
}


namespace Portal.Repository.Repositories
{

    public class Repository<TEntity> : BaseRepository, IRepository<TEntity> where TEntity : class
    {
        public async Task<ListResponse<TEntity>> GetAll()
        {
            using (var connection = _connection)
            {
                ListResponse<TEntity> response = new ListResponse<TEntity>();
                var query = "SELECT * FROM " + typeof(TEntity).Name;

                try
                {
                    _connection.Open();

                    if (IsConnectionOpen())
                    {
                        List<TEntity> items = _connection.Query<TEntity>(query, commandTimeout: 15).ToList();

                        if (items != null && items.Count > 0)
                        {
                            response.Items = items;
                        }
                        else
                        {
                            response.ErrorId = 1;
                            response.Message = "Falha ao consultar os status";
                        }
                    }
                    else
                    {
                        response.ErrorId = 1;
                        response.Message = "Falha ao conectar no banco de dados";
                    }
                }
                catch (Exception e)
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao consultar os status";
                }
                finally
                {
                    CloseConnection();
                }

                return response;
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            using (var connection = _connection)
            {
                var query = "SELECT * FROM " + typeof(TEntity).Name + " WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var result = await connection.QueryFirstOrDefaultAsync<TEntity>(query, parameters);
                return result;
            }
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            try
            {
                var properties = GetNonIdentityProperties(entity);
                string propertyNames = string.Join(", ", properties.Select(p => p.Name));
                string parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
                string query = $"INSERT INTO {typeof(TEntity).Name} ({propertyNames}) VALUES ({parameterNames}); SELECT * FROM {typeof(TEntity).Name} WHERE {typeof(TEntity).Name}Id = SCOPE_IDENTITY()";
                var parameters = GetDynamicParameters(entity, properties);

                if (_connection != null && _connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                if (_connection == null || _connection.State != ConnectionState.Open)
                {
                    return null;
                }

                var response = _connection.QueryFirstOrDefault<TEntity> (query, param: parameters);

                return response;
            }
            catch (Exception exception) { }
            finally
            {
                CloseConnection();
            }
            return null;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                var properties = GetNonIdentityProperties(entity);
                var propertySetters = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
                var query = $"UPDATE {typeof(TEntity).Name} SET {propertySetters} WHERE Id = @Id";
                var parameters = GetDynamicParameters(entity, properties);

                parameters.Add("@Id", GetIdPropertyValue(entity));

                if (_connection != null && _connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                if (_connection == null || _connection.State != ConnectionState.Open)
                {
                    return null;
                }
                var updatedEntity = await GetById(GetIdPropertyValue(entity));

                TEntity response = await _connection.QueryFirstOrDefaultAsync<TEntity>(query, parameters);

                return response;
            }
            catch (Exception e)
            {

                return default(TEntity);
            }
            finally
            {
                _connection.Close();
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var connection = _connection)
            {
                var query = "DELETE FROM " + typeof(TEntity).Name + " WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var result = await connection.ExecuteAsync(query, parameters);
                return result == 1;
            }
        }

        private DynamicParameters GetDynamicParameters(TEntity entity, IEnumerable<PropertyInfo> properties)
        {
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                if (value is int intValue && intValue == 0)
                {
                    parameters.Add("@" + property.Name, null);
                }
                else
                {
                    parameters.Add("@" + property.Name, value);
                }
            }
            return parameters;
        }

        private int GetIdPropertyValue(TEntity entity)
        {
            var properties = typeof(TEntity).GetProperties();
            var idProperty = properties.FirstOrDefault(p => p.Name.Contains($"{typeof(TEntity).Name}Id"));

            if (idProperty == null)
            {
                throw new KeyNotFoundException("A entidade não possui um ID.");
            }
            return (int)idProperty.GetValue(entity);
        }

        private IEnumerable<PropertyInfo> GetNonIdentityProperties(TEntity entity)
        {
            var properties = typeof(TEntity).GetProperties().Where(p => !p.Name.Contains($"{typeof(TEntity).Name}Id"));
            return properties;
        }
    }
}