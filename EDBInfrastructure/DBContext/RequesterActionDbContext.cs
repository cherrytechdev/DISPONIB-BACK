using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ESInfrastructure.Database;
using ESDomain.SeedWork;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using ESDomain.AggregateModels.ColdWorkPermitDetailAggregate;
using System.Globalization;
using System;
using ESDomain.AggregateModels.DocumentsDetailsAggregate;
using System.Security.Principal;
using Newtonsoft.Json;
using static System.Collections.Specialized.BitVector32;
using System.Drawing;
using ESDomain.AggregateModels.RequesterActionAggregate;

namespace ESInfrastructure.DBContext
{
    public class RequesterActionDbContext : IDBContext, IUnitOfWork
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;
        private readonly IConfiguration _configuration;
        private bool disposedValue = false;
        public RequesterActionDetail requesterActionDetail { get; set; }
        public UpdateRequesterActionDetail updateRequesterActionDetail { get; set; }

        private int result = 0;

        public RequesterActionDbContext(ISqlConnectionFactory sqlConnectionFactory, IConfiguration configuration)
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
                            command.CommandText = "uspRequesterActionDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@referenceid", SqlDbType.BigInt).Value = requesterActionDetail.referenceid;
                            command.Parameters.Add("@extentionrequired", SqlDbType.NVarChar).Value = requesterActionDetail.extentionrequired;
                            command.Parameters.Add("@validitydate", SqlDbType.NVarChar).Value = requesterActionDetail.validitydate;
                            command.Parameters.Add("@validitytime", SqlDbType.NVarChar).Value = requesterActionDetail.validitytime;
                            command.Parameters.Add("@processcompleted", SqlDbType.NVarChar).Value = requesterActionDetail.processcompleted;
                            command.Parameters.Add("@completecomments", SqlDbType.NVarChar).Value = requesterActionDetail.completecomments;
                            command.Parameters.Add("@housekeepingcheck", SqlDbType.Int).Value = requesterActionDetail.housekeepingcheck;
                            command.Parameters.Add("@handoverrequired", SqlDbType.NVarChar).Value = requesterActionDetail.handoverrequired;
                            command.Parameters.Add("@handoverrequestorid", SqlDbType.NVarChar).Value = requesterActionDetail.handoverrequestorid; 
                            command.Parameters.Add("@handoverreceiverid", SqlDbType.NVarChar).Value = requesterActionDetail.handoverreceiverid; 
                            command.Parameters.Add("@handoverissuerid", SqlDbType.NVarChar).Value = requesterActionDetail.handoverissuerid; 
                            command.Parameters.Add("@relievingreceivertype", SqlDbType.NVarChar).Value = requesterActionDetail.relievingreceivertype;
                            command.Parameters.Add("@handoverrequestortype", SqlDbType.NVarChar).Value = requesterActionDetail.handoverrequestortype;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = requesterActionDetail.userid;
                            command.Parameters.Add("@statusid", SqlDbType.Int).Value = requesterActionDetail.statusid;
                            int result = command.ExecuteNonQuery();
                        }
                        tr.Commit();

                        UserDetailsDataManagement(requesterActionDetail.handoverrequestorid, requesterActionDetail.handoverrequestorname, requesterActionDetail.handoverrequestoremail,
                             requesterActionDetail.handoverrequestorcontactno);

                        UserDetailsDataManagement(requesterActionDetail.handoverreceiverid, requesterActionDetail.handoverreceivername, requesterActionDetail.handoverreceiveremail,
                             requesterActionDetail.handoverreceivercontactno);

                        UserDetailsDataManagement(requesterActionDetail.handoverissuerid, requesterActionDetail.handoverissuername, requesterActionDetail.handoverissueremail,
                             requesterActionDetail.handoverissuercontactno);

                        ApprovalsDataManagement(Convert.ToInt64(requesterActionDetail.referenceid), Convert.ToInt32(requesterActionDetail.statusid)
                            , Convert.ToInt32(requesterActionDetail.statusid), requesterActionDetail.userid, "");
                        UpdateApprovalsStatus(Convert.ToInt64(requesterActionDetail.referenceid), Convert.ToInt32(requesterActionDetail.statusid));
                        EmailDataManagement(Convert.ToInt64(requesterActionDetail.referenceid), Convert.ToInt32(requesterActionDetail.statusid));
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

        public async Task<int> UserDetailsDataManagement(string username, string firstname, string email, string mobile)
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
                            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                            command.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = firstname;
                            command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = String.Empty;
                            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = String.Empty;
                            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                            command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = mobile;
                            command.Parameters.Add("@companyname", SqlDbType.NVarChar).Value = String.Empty;
                            command.Parameters.Add("@type", SqlDbType.Int).Value = 1;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = 1;

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

        private int UpdateApprovalsStatus(Int64 referenceid, int statusid, int requesterunderstand = 0)
        {
            using (var _conn = this.sqlConnectionFactory.GetOpenConnection())
            {
                using (var tran = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "uspUpdateApprovalsStatus";
                            command.Transaction = tran;
                            command.Parameters.Add("@referenceid", SqlDbType.BigInt).Value = Convert.ToInt64(referenceid);
                            command.Parameters.Add("@status", SqlDbType.Int).Value = statusid;
                            command.Parameters.Add("@requesterunderstand", SqlDbType.Int).Value = requesterunderstand;

                            int result = command.ExecuteNonQuery();
                            if (1 != result)
                            {
                                throw (new Exception("Failed to update record in database. Unknown error"));
                            }
                        }
                        tran.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return result;
                    }
                }
            }
        }

        private int ApprovalsDataManagement(Decimal referenceid, int statusid, int activityid, string userid, string comments)
        {
            using (var _conn = this.sqlConnectionFactory.GetOpenConnection())
            {
                using (var tran = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "uspApprovalsDataManagement";
                            command.Transaction = tran;
                            command.Parameters.Add("@referenceid", SqlDbType.BigInt).Value = Convert.ToInt64(referenceid);
                            command.Parameters.Add("@statusid", SqlDbType.Int).Value = statusid;
                            command.Parameters.Add("@activityid", SqlDbType.Int).Value = activityid;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = userid;
                            command.Parameters.Add("@comments", SqlDbType.NVarChar).Value = comments;
                            int result = command.ExecuteNonQuery();
                            if (1 != result)
                            {
                                throw (new Exception("Failed to update record in database. Unknown error"));
                            }
                        }
                        tran.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return result;
                    }
                }
            }
        }

        private int UpdateRequesterActionDataManagement(Decimal referenceid)
        {
            using (var _conn = this.sqlConnectionFactory.GetOpenConnection())
            {
                using (var tran = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "uspUpdateRequesterActionDataManagement";
                            command.Transaction = tran;
                            command.Parameters.Add("@referenceid", SqlDbType.BigInt).Value = Convert.ToInt64(referenceid);
                            command.Parameters.Add("@sitechecked", SqlDbType.Int).Value = updateRequesterActionDetail.sitechecked;
                            command.Parameters.Add("@immobilization", SqlDbType.NVarChar).Value = updateRequesterActionDetail.immobilization;
                            command.Parameters.Add("@deisolatedpcoiissuer", SqlDbType.NVarChar).Value = updateRequesterActionDetail.pcoiissuer;
                            command.Parameters.Add("@deisolatedecoiissuer", SqlDbType.NVarChar).Value = updateRequesterActionDetail.ecoiissuer;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = updateRequesterActionDetail.userid;
                            command.Parameters.Add("@deisolationcheck", SqlDbType.Int).Value = updateRequesterActionDetail.deisolationcheck;
                            command.Parameters.Add("@statusid", SqlDbType.Int).Value = updateRequesterActionDetail.statusid;
                            int result = command.ExecuteNonQuery();
                            if (1 != result)
                            {
                                throw (new Exception("Failed to update record in database. Unknown error"));
                            }
                        }
                        tran.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return result;
                    }
                }
            }
        }

        private void EmailDataManagement(Decimal referenceid, int action)
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
                            command.CommandText = "uspEmailDataManagement";
                            command.Transaction = tr;
                            command.Parameters.Add("@action", SqlDbType.Int).Value = action;
                            command.Parameters.Add("@referenceid", SqlDbType.BigInt).Value = referenceid;
                            command.ExecuteNonQuery();
                        }
                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                    }
                }
            }
        }
        public async Task<int> UpdateRecordAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ApprovalsDataManagement(Convert.ToInt64(updateRequesterActionDetail.referenceid), Convert.ToInt32(updateRequesterActionDetail.statusid)
                , Convert.ToInt32(updateRequesterActionDetail.statusid), updateRequesterActionDetail.userid, updateRequesterActionDetail.comments);
            if (Convert.ToInt32(updateRequesterActionDetail.statusid) == 11 || Convert.ToInt32(updateRequesterActionDetail.statusid) == 12)
                UpdateRequesterActionDataManagement(Convert.ToInt64(updateRequesterActionDetail.referenceid));
            int result = UpdateApprovalsStatus(Convert.ToInt64(updateRequesterActionDetail.referenceid), Convert.ToInt32(updateRequesterActionDetail.statusid),
                Convert.ToInt32(updateRequesterActionDetail.requesterunderstand));

            EmailDataManagement(Convert.ToInt64(updateRequesterActionDetail.referenceid), Convert.ToInt32(updateRequesterActionDetail.statusid));
            return result;
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