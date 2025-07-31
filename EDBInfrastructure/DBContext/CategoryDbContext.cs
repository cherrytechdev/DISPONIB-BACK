using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.SeedWork;
using ESInfrastructure.Database;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ESInfrastructure.DBContext
{
    public class CategoryDbContext : IDBContext, IUnitOfWork
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly IConfiguration _configuration;
        private bool disposedValue = false;
        public CategoryDetails categoryDetails { get; set; }
        private int result = 0;

        public CategoryDbContext(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
            this._configuration = configuration;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using (var _conn = this.sqlConnectionFactory.GetOpenConnection())
            {
                using (var tr = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = categoryDetails.categorycode == "0" ? "uspCategoryDataManagement" : "uspSubCategoryDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = categoryDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = 0;
                            command.Parameters.Add("@code", SqlDbType.NVarChar).Value = categoryDetails.code;
                            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = categoryDetails.description;
                            if (categoryDetails.categorycode != "0")
                                command.Parameters.Add("@categorycode", SqlDbType.NVarChar).Value = categoryDetails.categorycode;
                            int result = command.ExecuteNonQuery();
                            if (1 != result)
                            {
                                throw (new Exception("Failed to update record in database. Unknown error"));
                            }
                        }
                        tr.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        return result;
                    }
                }
            }
        }
        public async Task<int> UpdateRecordAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _conn = this.sqlConnectionFactory.GetOpenConnection())
            {
                using (var tr = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = categoryDetails.categorycode == "0" ? "uspCategoryDataManagement" : "uspSubCategoryDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 1;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = categoryDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = categoryDetails.id;
                            command.Parameters.Add("@code", SqlDbType.NVarChar).Value = categoryDetails.code;
                            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = categoryDetails.description;
                            if (categoryDetails.categorycode != "0")
                                command.Parameters.Add("@categorycode", SqlDbType.NVarChar).Value = categoryDetails.categorycode;
                            int result = command.ExecuteNonQuery();
                            if (1 != result)
                            {
                                throw (new Exception("Failed to update record in database. Unknown error"));
                            }
                        }
                        tr.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        return result;
                    }
                    return result;
                }
            }
        }
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public Task<List<Guid>> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<int> DeleteRecordAsync(CancellationToken cancellationToken = default)
        {
            using (var _conn = this.sqlConnectionFactory.GetOpenConnection())
            {
                using (var tr = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = categoryDetails.categorycode == "0" ? "uspCategoryDataManagement" : "uspSubCategoryDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = categoryDetails.action;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = categoryDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = categoryDetails.isactive;
                            command.Parameters.Add("@id", SqlDbType.BigInt).Value = categoryDetails.id;
                            int result = command.ExecuteNonQuery();
                            if (1 != result)
                            {
                                throw (new Exception("Failed to update record in database. Unknown error"));
                            }
                        }
                        tr.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        return result;
                    }
                    return result;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    //dispose managed state (managed objects).
                }
                this.disposedValue = true;
            }
        }
        public Task<bool> UpdateEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}