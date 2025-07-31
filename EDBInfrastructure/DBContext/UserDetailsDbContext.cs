using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ESInfrastructure.Database;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESDomain.SeedWork;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace ESInfrastructure.DBContext
{
    public class UserDetailsDbContext : IDBContext, IUnitOfWork
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly IConfiguration _configuration;
        private bool disposedValue = false;
        public UserDetail userDetails { get; set; }
        private int result = 0;

        public UserDetailsDbContext(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
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
                            command.CommandText = "uspUserDetailsDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = userDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@type", SqlDbType.Int).Value = userDetails.type;
                            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userDetails.username;
                            command.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = userDetails.firstname;
                            command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = userDetails.lastname;
                            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userDetails.password;
                            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userDetails.email;
                            command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = userDetails.mobile;
                            
                            command.Parameters.Add("@businessid", SqlDbType.NVarChar).Value = userDetails.businessid;
                            command.Parameters.Add("@reviews", SqlDbType.NVarChar).Value = userDetails.comment;
                            //command.Parameters.Add("@isquiz", SqlDbType.NVarChar).Value = userDetails.isquiz;
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
                            command.CommandText = "uspUserDetailsDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 1;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = userDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@type", SqlDbType.Int).Value = userDetails.type;
                            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userDetails.username;
                            command.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = userDetails.firstname;
                            command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = userDetails.lastname;
                            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userDetails.email;
                            command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = userDetails.mobile;                           
                            command.Parameters.Add("@businessid", SqlDbType.NVarChar).Value = userDetails.businessid;
                            command.Parameters.Add("@reviews", SqlDbType.NVarChar).Value = userDetails.comment;
                            command.Parameters.Add("@isquiz", SqlDbType.NVarChar).Value = userDetails.isquiz;
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
                            command.CommandText = "uspUserDetailsDataManagement";
                            command.Transaction = tr; 
                            command.Parameters.Add("@action", SqlDbType.Int).Value = userDetails.action;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = userDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = userDetails.isactive;
                            command.Parameters.Add("@type", SqlDbType.Int).Value = userDetails.type;
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