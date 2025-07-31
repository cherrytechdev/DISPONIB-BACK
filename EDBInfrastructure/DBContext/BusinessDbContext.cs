using ESDomain.AggregateModels.BusinessAggregate;
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
    public class BusinessDbContext : IDBContext, IUnitOfWork
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly IConfiguration _configuration;
        private bool disposedValue = false;
        public BusinessDetails businessDetails { get; set; }
        private int result = 0;

        public BusinessDbContext(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
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
                            command.CommandText = "uspBusinessDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = businessDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = 0;
                            command.Parameters.Add("@title", SqlDbType.NVarChar).Value = businessDetails.title;
                            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = businessDetails.description;
                            command.Parameters.Add("@address", SqlDbType.NVarChar).Value = businessDetails.address;
                            command.Parameters.Add("@categorycode", SqlDbType.NVarChar).Value = businessDetails.categorycode;
                            command.Parameters.Add("@subcategorycode", SqlDbType.NVarChar).Value = businessDetails.subcategorycode; 
                            command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = businessDetails.mobile;
                            command.Parameters.Add("@whatsapp", SqlDbType.NVarChar).Value = businessDetails.whatsapp;
                            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = businessDetails.email;
                            command.Parameters.Add("@alternativeemail", SqlDbType.NVarChar).Value = businessDetails.alternativeemail;
                            command.Parameters.Add("@website", SqlDbType.NVarChar).Value = businessDetails.website;
                            command.Parameters.Add("@instagram", SqlDbType.NVarChar).Value = businessDetails.instagram;
                            command.Parameters.Add("@facebook", SqlDbType.NVarChar).Value = businessDetails.facebook;
                            command.Parameters.Add("@linkedin", SqlDbType.NVarChar).Value = businessDetails.linkedin;
                            command.Parameters.Add("@couponcode", SqlDbType.NVarChar).Value = businessDetails.couponcode;
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
                            command.CommandText = "uspBusinessDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 1;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = businessDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = businessDetails.id; 
                            command.Parameters.Add("@title", SqlDbType.NVarChar).Value = businessDetails.title;
                            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = businessDetails.description;
                            command.Parameters.Add("@address", SqlDbType.NVarChar).Value = businessDetails.address;
                            command.Parameters.Add("@categorycode", SqlDbType.NVarChar).Value = businessDetails.categorycode;
                            command.Parameters.Add("@subcategorycode", SqlDbType.NVarChar).Value = businessDetails.subcategorycode;
                            command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = businessDetails.mobile;
                            command.Parameters.Add("@whatsapp", SqlDbType.NVarChar).Value = businessDetails.whatsapp;
                            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = businessDetails.email;
                            command.Parameters.Add("@alternativeemail", SqlDbType.NVarChar).Value = businessDetails.alternativeemail;
                            command.Parameters.Add("@website", SqlDbType.NVarChar).Value = businessDetails.website;
                            command.Parameters.Add("@instagram", SqlDbType.NVarChar).Value = businessDetails.instagram;
                            command.Parameters.Add("@facebook", SqlDbType.NVarChar).Value = businessDetails.facebook;
                            command.Parameters.Add("@linkedin", SqlDbType.NVarChar).Value = businessDetails.linkedin;
                            command.Parameters.Add("@couponcode", SqlDbType.NVarChar).Value = businessDetails.couponcode;
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
                            command.CommandText = "uspBusinessDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = businessDetails.action;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = businessDetails.userid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = businessDetails.isactive;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = businessDetails.id;
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