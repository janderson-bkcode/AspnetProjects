using Dapper;
using Domain.Portal.Interface.Repository;
using Domain.Portal.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Portal.Repository.Repositories
{
    public class Repository<TEntity> : BaseRepository, IRepository<TEntity> where TEntity : class
    {
        public async Task<ListResponse<TEntity>> GetAll(string sinceDate = null, string untilDate = null, string[] properties = null)
        {
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                var tableName = GetTableName();

                ListResponse<TEntity> response = new ListResponse<TEntity>();

                string query;

                if (sinceDate != null && untilDate != null && properties.Length > 0)
                {
                    query = $"SELECT * FROM {tableName} WHERE {properties[0]} >= '{sinceDate}' AND {properties[0]} <= '{untilDate}' AND {properties[1]} = 1 AND {properties[2]} IS NOT NULL " +
                        $"ORDER BY {tableName}Id";
                }
                else if (sinceDate != null && untilDate == null && properties.Length > 0)
                {
                    query = $"SELECT * FROM {tableName} WHERE {properties[0]} >= '{sinceDate}' AND {properties[1]} = 1 AND {properties[2]} IS NOT NULL " +
                        $"ORDER BY {tableName}Id";
                }
                else
                {
                    query = "SELECT * FROM " + tableName;
                }
                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        List<TEntity> items = connection.Query<TEntity>(query, commandTimeout: 15).ToList();

                        if (items != null && items.Count > 0)
                        {
                            response.Items = items;
                            return response;
                        }
                        else
                        {
                            response.ErrorId = 1;
                            response.Message = "Falha ao consultar os status";
                            return null;
                        }
                    }
                    else
                    {
                        response.ErrorId = 1;
                        response.Message = "Falha ao conectar no banco de dados";
                        return null;
                    }
                }
                catch (Exception e)
                {
                    response.ErrorId = 500;
                    response.Message = "Falha ao consultar os status";
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<ListResponse<TEntity>> GetPagined(int page, int pageSize, string sinceDate = null, string untilDate = null, string[] properties = null)
        {
            var tableName = GetTableName();
            var startIndex = (page - 1) * pageSize;

            string query;

            if (sinceDate != null && untilDate != null && properties.Length > 0)
            {
                query = $"SELECT * FROM {tableName} WHERE {properties[0]} >= '{sinceDate}' AND {properties[0]} <= '{untilDate}' AND {properties[1]} = 1 AND {properties[2]} IS NOT NULL " +
                    $"ORDER BY {tableName}Id";
            }
            else if (sinceDate != null && untilDate == null && properties.Length > 0)
            {
                query = $"SELECT * FROM {tableName} WHERE {properties[0]} >= '{sinceDate}' AND {properties[1]} = 1 AND {properties[2]} IS NOT NULL " +
                    $"ORDER BY {tableName}Id";
            }
            else
            {
                query = "SELECT * FROM " + tableName;
            }
            ListResponse<TEntity> response = new ListResponse<TEntity>();

            try
            {
                using (var connection = new SqlConnection(_connection.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 15;
                        var entities = new List<TEntity>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var propertiesEntity = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                            var propertyNames = Enumerable.Range(0, reader.FieldCount)
                                                          .Select(reader.GetName)
                                                          .ToList();

                            while (await reader.ReadAsync())
                            {
                                var entity = Activator.CreateInstance<TEntity>();

                                Parallel.ForEach(propertiesEntity, property =>
                                {
                                    if (propertyNames.Contains(property.Name) && !reader.IsDBNull(reader.GetOrdinal(property.Name)))
                                    {
                                        var value = reader[property.Name];

                                        if (property.PropertyType == typeof(double) && value.GetType() == typeof(decimal))
                                        {
                                            value = Convert.ToDouble(value);
                                        }

                                        property.SetValue(entity, value);
                                    }
                                });
                                entities.Add(entity);
                                response.Items = entities;
                            }
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                response.ErrorId = 500;
                response.Message = "Falha ao consultar os status";
                return null;
            }
        }

        public async Task<ListResponse<TEntity>> GetAllPaged(int pageSize, string sinceDate = null, string untilDate = null)
        {
            ListResponse<TEntity> result = new ListResponse<TEntity>();
            var page = 1;

            while (true)
            {
                var items = GetPagined(page, pageSize, sinceDate, untilDate).Result;

                if (items.Items.Count == 0)
                {
                    break;
                }
                result.Items.AddRange(items.Items);

                page++;
            }

            return result;
        }

        public async Task<TEntity> GetById(int id)
        {
            BaseResponse response = new BaseResponse();
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                connection.Open();

                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        var tableName = GetTableName();
                        var parameters = new DynamicParameters();

                        parameters.Add("@Id", id);
                        var query = $"SELECT * FROM {tableName} WHERE {tableName}Id = @Id";
                        var result = connection.QueryFirstOrDefaultAsync<TEntity>(query, parameters, commandTimeout: 15).Result;

                        return result;
                    }
                    else
                    {
                        response.ErrorId = 1;
                        response.Message = "Não foi possível conectar-se com o banco de dados.";
                        return null;
                    }
                }
                catch (Exception e)
                {
                    response.ErrorId = 1;
                    response.Message = "Falha ao consultar os status";
                    return null;
                }
                finally
                {
                    connection.Close();
                }
                return null;
            }
        }

        public async Task<TEntity> GetByProperty(string property, string value)
        {
            BaseResponse response = new BaseResponse();
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                connection.Open();

                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        var tableName = GetTableName();
                        var parameters = new DynamicParameters();

                        parameters.Add($"@{property}", value);
                        var query = $"SELECT * FROM {tableName} WHERE {property} = @{property}";
                        var result = connection.QueryFirstOrDefaultAsync<TEntity>(query, parameters, commandTimeout: 15).Result;

                        return result;
                    }
                    else
                    {
                        response.ErrorId = 1;
                        response.Message = "Não foi possível conectar-se com o banco de dados.";
                        return null;
                    }
                }
                catch (Exception e)
                {
                    response.ErrorId = 1;
                    response.Message = "Falha ao consultar";
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            BaseResponse response = new BaseResponse();
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                connection.Open();

                if (connection.State != ConnectionState.Open)
                {
                    response.ErrorId = 1;
                    response.Message = "Não foi possível conectar-se com o banco de dados.";
                    return null;
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var tableName = GetTableName();
                        var columnNames = GetColumnNames().ToList();
                        var properties = GetNonIdentityProperties(entity).ToList();
                        var parameters = GetDynamicParameters(entity, properties);

                        var nestedObjects = entity.GetType().GetProperties()
                            .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string))
                            .ToList();
                        foreach (var property in nestedObjects)
                        {
                            var nestedObject = property.GetValue(entity);
                            var nameClassNested = nestedObject.GetType().Name;
                            var nestedObjectProperties = nestedObject.GetType().GetProperties().Where(n => n.Name.Contains($"{nameClassNested}Id"));
                            foreach (var nestedProperty in nestedObjectProperties)
                            {
                                // Serializando propriedades Id da classe Aninhada
                                var json = JsonConvert.SerializeObject(nestedProperty.GetValue(nestedObject));
                                parameters.Add($"@{nestedProperty.Name}", json);
                                columnNames.Add(nestedProperty.Name);
                                properties.Add(nestedProperty);
                            }
                        }

                        var columnList = string.Join(",", columnNames);
                        var parameterList = string.Join(", ", properties.Select(p => "@" + p.Name));

                        var insertQuery = $"INSERT INTO {tableName} ({columnList}) VALUES ({parameterList});SELECT * FROM {tableName} WHERE {tableName}Id = SCOPE_IDENTITY()";

                        TEntity insertedEntity = connection.QueryFirstOrDefault<TEntity>(insertQuery, parameters, transaction, commandTimeout: 15);

                        transaction.Commit();

                        return insertedEntity;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.ErrorId = 1;
                        response.Message = "Falha ao inserir dados";
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
         public async Task<TEntity> InsertWithProcedure(TEntity entity, string propertyProcedureName)
        {
            BaseResponse response = new BaseResponse();
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    response.ErrorId = 1;
                    response.Message = "Não foi possível conectar-se com o banco de dados.";
                    return null;
                }

                try
                {
                    var tableName = GetTableName();
                    var columnNames = GetColumnNames().ToList();
                    var properties = GetNonIdentityProperties(entity).ToList();
                    var parameters = GetDynamicParameters(entity, properties);

                    var nestedObjects = entity.GetType().GetProperties()
                        .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string))
                        .ToList();
                    foreach (var property in nestedObjects)
                    {
                        var nestedObject = property.GetValue(entity);
                        var nameClassNested = nestedObject.GetType().Name;
                        var nestedObjectProperties = nestedObject.GetType().GetProperties().Where(n => n.Name.Contains($"{nameClassNested}Id"));
                        foreach (var nestedProperty in nestedObjectProperties)
                        {
                            // Serializando propriedades Id da classe Aninhada
                            var json = JsonConvert.SerializeObject(nestedProperty.GetValue(nestedObject));
                            parameters.Add($"@{nestedProperty.Name}", json);
                            columnNames.Add(nestedProperty.Name);
                            properties.Add(nestedProperty);
                        }
                    }

                    if (propertyProcedureName != null)
                    {
                        // parâmetro de saída para a procedure que irá receber o valor retornado:

                        var outParam = new DynamicParameters();
                        outParam.Add("@OutputParamName", dbType: DbType.Int32, direction: ParameterDirection.Output);

                        // Chama a procedure para obter o valor a ser inserido na coluna
                        connection.ExecuteScalarAsync<int>("NomeDaSuaProcedure", commandType: CommandType.StoredProcedure).Wait();
                        var propertyName = propertyProcedureName; // substitua pelo nome da propriedade que deseja atualizar

                        // Atribuindo o valor da procedure a variavel
                        var propertyProcedure = typeof(TEntity).GetProperty(propertyName);
                        propertyProcedure.SetValue(entity, outParam.Get<int>("@OutputParamName"));

                        columnNames.Add(propertyName); // adiciona o nome da propriedade
                        parameters.Add($"@{propertyName}", propertyProcedure.GetValue(entity)); // adiciona o valor da propriedade como parâmetro
                    }

                    var columnList = string.Join(",", columnNames);
                    var parameterList = string.Join(", ", properties.Select(p => "@" + p.Name));

                    var insertQuery = $"INSERT INTO {tableName} ({columnList}) VALUES ({parameterList});SELECT * FROM {tableName} WHERE {tableName}Id = SCOPE_IDENTITY()";

                    TEntity insertedEntity = await connection.QueryFirstOrDefaultAsync<TEntity>(insertQuery, parameters, commandTimeout: 15);

                    return insertedEntity;
                }
                catch (Exception ex)
                {
                    response.ErrorId = 1;
                    response.Message = "Falha ao inserir dados";
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            BaseResponse response = new BaseResponse();
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                connection.Open();

                if (connection.State != ConnectionState.Open)
                {
                    response.ErrorId = 1;
                    response.Message = "Não foi possível conectar-se com o banco de dados.";
                    return null;
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var tableName = GetTableName();
                        var columnNames = GetColumnNames().ToList();
                        var properties = GetNonIdentityProperties(entity).ToList();
                        var parameters = GetDynamicParameters(entity, properties);

                        parameters.Add("@Id", GetIdPropertyValue(entity));

                        var nestedObjects = entity.GetType().GetProperties()
                            .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string))
                            .ToList();
                        foreach (var property in nestedObjects)
                        {
                            var nestedObject = property.GetValue(entity);
                            var nameClassNested = nestedObject.GetType().Name;
                            var nestedObjectProperties = nestedObject.GetType().GetProperties().Where(n => n.Name.Contains($"{nameClassNested}Id"));
                            foreach (var nestedProperty in nestedObjectProperties)
                            {
                                // Serializando propriedades Id da classe Aninhada
                                var json = JsonConvert.SerializeObject(nestedProperty.GetValue(nestedObject));
                                parameters.Add($"@{nestedProperty.Name}", json);
                                columnNames.Add(nestedProperty.Name);
                                properties.Add(nestedProperty);
                            }
                        }

                        var columnList = string.Join(",", columnNames);
                        var parameterList = string.Join(", ", properties.Select(p => "@" + p.Name));

                        var updateQuery = $"UPDATE {tableName} SET {string.Join(", ", properties.Select(p => p.Name + " = @" + p.Name))} WHERE {tableName}Id = @Id; " + $"SELECT * FROM {tableName} WHERE {tableName}Id = @Id";

                        TEntity updatedEntity = connection.QueryFirstOrDefaultAsync<TEntity>(updateQuery, parameters, commandTimeout: 15, transaction: transaction).Result;

                        transaction.Commit();

                        return updatedEntity;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.ErrorId = 1;
                        response.Message = "Falha ao atualizar dados";
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            BaseResponse response = new BaseResponse();
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                connection.Open();

                if (connection.State != ConnectionState.Open)
                {
                    response.ErrorId = 1;
                    response.Message = "Não foi possível conectar-se com o banco de dados.";
                    return null;
                }
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var tableName = GetTableName();
                        var properties = GetNonIdentityProperties(entity);
                        var parameters = GetDynamicParameters(entity, properties);

                        parameters.Add("@Id", GetIdPropertyValue(entity));
                        var deleteQuery = $"DELETE FROM {tableName} WHERE {tableName}Id = @Id;" + $"SELECT * FROM {tableName} WHERE {tableName}Id = @Id";

                        TEntity deletedEntity = connection.QueryFirstOrDefaultAsync<TEntity>(deleteQuery, parameters, commandTimeout: 15, transaction: transaction).Result;

                        transaction.Commit();

                        return deletedEntity;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();

                        response.ErrorId = 1;
                        response.Message = "Falha ao deletar dados";
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return null;
                }
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

        private string GetTableName()
        {
            var entityType = typeof(TEntity);
            var tableNameAttr = entityType.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
            var tableName = tableNameAttr?.Name ?? entityType.Name;

            return tableName;
        }

        private IEnumerable<string> GetColumnNames()
        {
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties().Where(p => !p.Name.Contains($"{typeof(TEntity).Name}Id") && !p.PropertyType.IsClass && !p.PropertyType.IsNested || p.PropertyType == typeof(string));
            var columnNames = new List<string>();
            foreach (var property in properties)
            {
                var columnAttr = property.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() as ColumnAttribute;
                var columnName = columnAttr?.Name ?? property.Name;
                columnNames.Add(columnName);
            }
            return columnNames;
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
            var properties = typeof(TEntity).GetProperties().Where(p => !p.Name.Contains($"{typeof(TEntity).Name}Id") && !p.PropertyType.IsClass && !p.PropertyType.IsNested || p.PropertyType == typeof(string));
            return properties;
        }
    }
}