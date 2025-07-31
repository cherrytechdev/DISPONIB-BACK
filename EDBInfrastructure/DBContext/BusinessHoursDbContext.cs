
using ESDomain.AggregateModels.BusinessHoursAggregate;
using ESDomain.SeedWork;
using ESInfrastructure.Database;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ESInfrastructure.DBContext
{
    public class BusinessHoursDbContext : IDBContext, IUnitOfWork
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly IConfiguration _configuration;
        private bool disposedValue = false;
        public BusinessHoursDetails businessHoursDetails { get; set; }
        private int result = 0;

        public BusinessHoursDbContext(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
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
                            command.CommandText = "uspBusinessHoursDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = businessHoursDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = 0;
                            command.Parameters.Add("@businessid", SqlDbType.NVarChar).Value = businessHoursDetails.businessid;
                            command.Parameters.Add("@day", SqlDbType.NVarChar).Value = businessHoursDetails.day;
                            command.Parameters.Add("@starttime", SqlDbType.NVarChar).Value = businessHoursDetails.starttime;
                            command.Parameters.Add("@endtime", SqlDbType.NVarChar).Value = businessHoursDetails.endtime;
                            command.Parameters.Add("@status", SqlDbType.Int).Value = businessHoursDetails.status;
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
                            command.CommandText = "uspBusinessHoursDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 1;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = businessHoursDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = businessHoursDetails.id;
                            command.Parameters.Add("@businessid", SqlDbType.NVarChar).Value = businessHoursDetails.businessid;
                            command.Parameters.Add("@day", SqlDbType.NVarChar).Value = businessHoursDetails.day;
                            command.Parameters.Add("@starttime", SqlDbType.NVarChar).Value = businessHoursDetails.starttime;
                            command.Parameters.Add("@endtime", SqlDbType.NVarChar).Value = businessHoursDetails.endtime;
                            command.Parameters.Add("@status", SqlDbType.Int).Value = businessHoursDetails.status;
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
                            command.CommandText = "uspBusinessHoursDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = businessHoursDetails.action;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = businessHoursDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = businessHoursDetails.isactive;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = businessHoursDetails.id;
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