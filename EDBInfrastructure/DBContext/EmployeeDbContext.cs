using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ESInfrastructure.Database;
using ESDomain.AggregateModels.EmployeeDetailAggregate;
using ESDomain.SeedWork;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace ESInfrastructure.DBContext
{
    public class EmployeeDbContext : IDBContext, IUnitOfWork
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly IConfiguration _configuration;
        private bool disposedValue = false;
        public EmployeeDetail userDetails { get; set; }
        private int result = 0;

        public EmployeeDbContext(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
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
                            command.CommandText = "uspEmployeeDataManagement";
                            command.Transaction = tr; 
                            command.Parameters.Add("@empname", SqlDbType.NVarChar).Value = userDetails.empname;  
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
            using (var conn = this.sqlConnectionFactory.GetOpenConnection())
            {
                using (var tr = conn.BeginTransaction())
                {
                    try
                    {

                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        return 0;
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
            throw new NotImplementedException();
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