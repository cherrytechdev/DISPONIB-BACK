using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http; 

namespace ESApplication.Queries.GetSiteDataDetails
{
    public class GetSiteDataDetailsQueryHandler : IRequestHandler<GetSiteDataDetailsQuery, List<SiteDataDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetSiteDataDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<SiteDataDto>> Handle(GetSiteDataDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<SiteDataDto>();
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
                            command.CommandText = "uspGetSiteData";
                            command.Transaction = tr;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = request.userid;
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

        public SiteDataDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new SiteDataDto()
            {
                userid = row["userid"].ToString(),
                id = row["id"].ToString(),
                type = row["type"].ToString(),
                filename = row["filename"].ToString(),
                description = row["description"].ToString(),
                base64text = row["base64text"].ToString(), 
                createdon = Convert.ToDateTime(row["createdon"]),
                filepath = Convert.ToString(row["filepath"])
            };
            return parentsDetail;
        }
    }
}
 