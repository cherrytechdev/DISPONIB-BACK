using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using ESApplication.Queries.GetApprovalsDetails; 

namespace ESApplication.Queries.GetApprovalsDetails
{
    public class GetApprovalsDetailsQueryHandler : IRequestHandler<GetApprovalsDetailsQuery, List<ApprovalsDetailsDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetApprovalsDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ApprovalsDetailsDto>> Handle(GetApprovalsDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<ApprovalsDetailsDto>();
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
                            command.CommandText = "uspGetApprovalsDetails";
                            command.Parameters.Add("@referenceid", SqlDbType.NVarChar).Value = request.referenceid;
                            command.Parameters.Add("@permittype", SqlDbType.Int).Value = request.permittype;
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

        public ApprovalsDetailsDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new ApprovalsDetailsDto()
            {
                referenceid = row["referenceid"].ToString(),
                id = Convert.ToInt64(row["id"]),  
                statusid = Convert.ToInt32(row["statusid"]),
                status = row["status"].ToString(),
                activityid = Convert.ToInt32(row["activityid"]),
                activity = row["activity"].ToString(),
                comments = row["comments"].ToString(),
                actionon = Convert.ToDateTime(row["actionon"]),
                actionby = row["actionby"].ToString(),
                modifiedon = Convert.ToDateTime(row["modifiedon"]),
                modifiedby = row["modifiedby"].ToString() 
            };
            return parentsDetail;
        }
    }
}
  