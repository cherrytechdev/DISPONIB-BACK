using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http; 

namespace ESApplication.Queries.GetCommonDetails
{
    public class GetCommonDetailsQueryHandler : IRequestHandler<GetCommonDetailsQuery, List<CommonDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCommonDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<CommonDto>> Handle(GetCommonDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<CommonDto>();
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
                            command.CommandText = "uspGetCommonDetails";
                            command.Transaction = tr;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = request.userid;
                            command.Parameters.Add("@type", SqlDbType.Int).Value = request.type;
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

        public CommonDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new CommonDto()
            {
                businessid = row["businessid"].ToString(),
                count = Convert.ToInt16(row["count"]),
                username = row["username"].ToString(),
                title = row["title"].ToString(),
                createdon = Convert.ToDateTime(row["createdon"]),
            };
            return parentsDetail;
        }
    }
}
 