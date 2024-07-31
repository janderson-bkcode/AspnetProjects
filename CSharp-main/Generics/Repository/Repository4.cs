using Dapper;
using Domain.Portal.Interface.Repository;
using Domain.Portal.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

        //public async Task<TEntity> Insert(TEntity entity)
        //{
        //    var properties = GetNonIdentityProperties(entity);
        //    string propertyNames = string.Join(", ", properties.Select(p => p.Name));
        //    string parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
        //    string query = $"INSERT INTO {typeof(TEntity).Name} ({propertyNames}) VALUES ({parameterNames}); SELECT * FROM {typeof(TEntity).Name} WHERE {typeof(TEntity).Name}Id = SCOPE_IDENTITY()";
        //    using (var connection = new SqlConnection(_connection.ConnectionString))
        //    {
        //        if (connection != null && connection.State != ConnectionState.Open) connection.Open();

        //        if (connection == null || connection.State != ConnectionState.Open) return null;

        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Atualiza classe agregada, se houver
        //                var foreignKeyProperties = properties.Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string));
        //                foreach (var foreignKeyProperty in foreignKeyProperties)
        //                {
        //                    var foreignEntity = foreignKeyProperty.GetValue(entity);

        //                    if (foreignEntity != null)
        //                    {
        //                        var foreignRepository = new Repository<object>();
        //                        var foreignResult = foreignRepository.Insert(foreignEntity).Result;
        //                        var foreignIdProperty = foreignEntity.GetType().GetProperty($"{typeof(TEntity).Name}Id");
        //                        foreignIdProperty.SetValue(foreignEntity, foreignResult);
        //                        foreignKeyProperty.SetValue(entity, foreignEntity);
        //                    }
        //                }
        //                var parameters = GetDynamicParameters(entity, properties);

        //                var response = _connection.QueryFirstOrDefault<TEntity>(query, param: parameters);

        //                transaction.Commit();

        //                return response;
        //            }
        //            catch (Exception exception)
        //            {
        //                transaction.Rollback();
        //            }
        //            finally
        //            {
        //                CloseConnection();
        //            }
        //        }
        //    }
        //    return null;
        //}
        public async Task<TEntity> Insert(TEntity entity)
        {
            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var tableName = GetTableName();
                        var columnNames = GetColumnNames().ToList();
                        var properties = GetNonIdentityProperties(entity);
                        var columnList = string.Join(",", columnNames);
                        var parameterList = string.Join(", ", properties.Select(p => "@" + p.Name));
                        var parameters = GetDynamicParameters(entity, properties);

                        // Serializando propriedades de classes agregadas e colocando no JSON
                        var nestedObjects = entity.GetType().GetProperties()
                            .Where(p => p.PropertyType.IsClass &&  p.PropertyType != typeof(string))
                            .ToList();
                        foreach (var property in nestedObjects)
                        {
                            var json = JsonConvert.SerializeObject(property.GetValue(entity));
                            parameters.Add($"@{property.Name}", json);
                            columnNames.Add(property.Name);
                        }

                       
                        var insertQuery = $"INSERT INTO {tableName} ({columnList}) VALUES ({parameterList});SELECT * FROM {tableName} WHERE {tableName}Id = SCOPE_IDENTITY()";

                        var insertedEntity = connection.QueryFirstOrDefault<TEntity>(insertQuery, parameters, transaction);

                        transaction.Commit();

                        return insertedEntity;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error inserting entity", ex);
                    }
                }
            }
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
            var properties = entityType.GetProperties().Where(p => !p.Name.Contains($"{typeof(TEntity).Name}Id"));
            var columnNames = new List<string>();
            foreach (var property in properties)
            {
                var columnAttr = property.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() as ColumnAttribute;
                var columnName = columnAttr?.Name ?? property.Name;
                columnNames.Add(columnName);
            }
            return columnNames;
        }

        private DynamicParameters GetDynamicParameters(TEntity entity)
        {
            var parameters = new DynamicParameters();
            var entityType = entity.GetType();
            var properties = GetNonIdentityProperties(entity);

            foreach (var property in properties)
            {
                var columnAttr = property.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() as ColumnAttribute;
                var columnName = columnAttr?.Name ?? property.Name;
                var value = property.GetValue(entity);

                //  Se a coluna tem valor zero e a coluna permite nulo, seta null
                if (value is int && ((int)value) == 0)
                {
                    var allowDBNullAttr = property.GetCustomAttributes(typeof(AllowNullAttribute), true).FirstOrDefault() as AllowNullAttribute;
                    if (allowDBNullAttr != null)
                    {
                        value = null;
                    }
                }

                parameters.Add(columnName, value);
            }
            return parameters;
        }

        private int GetIdPropertyValue(TEntity entity)
        {
            var properties = GetNonIdentityProperties(entity);
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