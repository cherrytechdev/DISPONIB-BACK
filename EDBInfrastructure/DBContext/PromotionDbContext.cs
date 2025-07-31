using ESDomain.AggregateModels.CategoryAggregate;
using ESDomain.AggregateModels.PromotionAggregate;
using ESDomain.SeedWork;
using ESInfrastructure.Database;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ESInfrastructure.DBContext
{
    public class PromotionDbContext : IDBContext, IUnitOfWork
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly IConfiguration _configuration;
        private bool disposedValue = false;
        public PromotionDetails promotionDetails { get; set; }
        private int result = 0;

        public PromotionDbContext(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
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
                            command.CommandText = "uspPromotionDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = promotionDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = 0;
                            command.Parameters.Add("@couponcode", SqlDbType.NVarChar).Value = promotionDetails.couponcode;
                            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = promotionDetails.description;
                            command.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = promotionDetails.startdate;
                            command.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = promotionDetails.enddate;
                            command.Parameters.Add("@discount", SqlDbType.NVarChar).Value = promotionDetails.discount;
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
                            command.CommandText = "uspPromotionDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 1;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = promotionDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = promotionDetails.id;
                            command.Parameters.Add("@couponcode", SqlDbType.NVarChar).Value = promotionDetails.couponcode;
                            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = promotionDetails.description;
                            command.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = promotionDetails.startdate;
                            command.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = promotionDetails.enddate;
                            command.Parameters.Add("@discount", SqlDbType.NVarChar).Value = promotionDetails.discount;
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
                            command.CommandText = "uspPromotionDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = promotionDetails.action;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = promotionDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = promotionDetails.isactive;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = promotionDetails.id;
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