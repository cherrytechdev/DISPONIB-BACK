using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ESInfrastructure.Database;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.SeedWork;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using ESDomain.AggregateModels.SiteDataAggregate;

namespace ESInfrastructure.DBContext
{
    public class SiteDataDbContext : IDBContext, IUnitOfWork
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly IConfiguration _configuration;
        private bool disposedValue = false;
        public SiteDataDetails siteDataDetails { get; set; }
        private int result = 0;

        public SiteDataDbContext(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
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
                            command.CommandText = "uspSiteDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = 0;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = siteDataDetails.userid;
                            command.Parameters.Add("@type", SqlDbType.NVarChar).Value = siteDataDetails.type;
                            command.Parameters.Add("@filename", SqlDbType.NVarChar).Value = siteDataDetails.filename;
                            command.Parameters.Add("@filepath", SqlDbType.NVarChar).Value = siteDataDetails.filepath;
                            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = siteDataDetails.description;
                            command.Parameters.Add("@base64text", SqlDbType.NVarChar).Value = siteDataDetails.base64text;

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
                            command.CommandText = "uspSiteDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 1;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = siteDataDetails.id;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = siteDataDetails.userid;
                            command.Parameters.Add("@type", SqlDbType.NVarChar).Value = siteDataDetails.type;
                            command.Parameters.Add("@filename", SqlDbType.NVarChar).Value = siteDataDetails.filename;
                            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = siteDataDetails.description;
                            command.Parameters.Add("@base64text", SqlDbType.NVarChar).Value = siteDataDetails.base64text;  
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
                            command.CommandText = "uspSiteDataManagement";
                            command.Transaction = tr; 
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 2;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = siteDataDetails.id;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = siteDataDetails.userid; 
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