using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using ESApplication.Queries.GetDocumentsDetails; 

namespace ESApplication.Queries.GetRequesterAction
{
    public class GetRequesterActionQueryHandler : IRequestHandler<GetRequesterActionQuery, List<RequesterActionDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetRequesterActionQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<RequesterActionDto>> Handle(GetRequesterActionQuery request, CancellationToken cancellationToken)
        {
            var response = new List<RequesterActionDto>();
            using (var _conn = this._sqlConnectionFactory.GetOpenConnection())
            {
                using (var tr = _conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = _conn;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "uspGetRequesterAction";
                            command.Parameters.Add("@referenceid", SqlDbType.NVarChar).Value = request.referenceid; 
                            command.Transaction = tr;
                            DataTable dt = new DataTable();

                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                            {
                                dataAdapter.SelectCommand = command;
                                dataAdapter.Fill(dt);
                            }

                            foreach (DataRow row in dt.Rows)
                            {
                                response.Add(CreateFromSqlDataReader(row));
                            }
                        }
                        tr.Commit();
                        return response;
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        return response;
                    }
                }
            }
        }

        public RequesterActionDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new RequesterActionDto()
            {
                referenceid = Convert.ToInt64(row["referenceid"]),
                extentionrequired = row["extentionrequired"].ToString(),
                validitydate = row["validitydate"].ToString(),
                validitytime = row["validitytime"].ToString(),
                processcompleted = row["processcompleted"].ToString(),              
                completecomments = row["completecomments"].ToString(),
                housekeepingcheck = Convert.ToInt32(row["housekeepingcheck"]),
                handoverrequired = row["handoverrequired"].ToString(),
                handoverrequestorid = row["handoverrequestorid"].ToString(), 
                handoverrequestorname = row["handoverrequestorname"].ToString(), 
                handoverrequestoremail = row["handoverrequestoremail"].ToString(), 
                handoverrequestorcontactno = row["handoverrequestorcontactno"].ToString(),
                handoverreceiverid = row["handoverreceiverid"].ToString(),
                handoverreceivercontactno = row["handoverreceivercontactno"].ToString(),
                handoverreceivername = row["handoverreceivername"].ToString(),
                handoverreceiveremail = row["handoverreceiveremail"].ToString(),
                handoverissuerid = row["handoverissuerid"].ToString(),
                handoverissuername = row["handoverissuername"].ToString(),
                handoverissueremail = row["handoverissueremail"].ToString(),
                handoverissuercontactno = row["handoverissuercontactno"].ToString(),
                relievingreceivertype = row["relievingreceivertype"].ToString(),
                handoverrequestortype = row["handoverrequestortype"].ToString(),
                createdby = row["createdby"].ToString(),
                permitissuername = row["permitissuername"].ToString(),
                requestorname = row["requestorname"].ToString(),
                permitreceivername = row["permitreceivername"].ToString(),

            };
            return parentsDetail;
        }
    }
} 