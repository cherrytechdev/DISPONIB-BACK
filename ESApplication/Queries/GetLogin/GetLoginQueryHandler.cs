using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESDomain.AggregateModels.UserDetailsAggregate;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ESApplication.Queries.GetLogin
{
    public class GetLoginQueryHandler : IRequestHandler<GetLoginQuery, List<UserDetailsDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetLoginQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<UserDetailsDto>> Handle(GetLoginQuery request, CancellationToken cancellationToken)
        {
            var response = new List<UserDetailsDto>();
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
                            command.CommandText = "uspLogin";
                            command.Transaction = tr;
                            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = request.username;
                            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = Utilities.Encrypt(request.password);

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

        public UserDetailsDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new UserDetailsDto()
            {
                userid =row["userid"].ToString(),
                roleid = row["roleid"].ToString(),
                username = row["username"].ToString(),
                //password = row["password"].ToString(),
                firstname = row["firstname"].ToString(),
                lastname = row["lastname"].ToString(),
                email = row["email"].ToString(),
                mobile = row["mobile"].ToString(), 
            };
            return parentsDetail;
        }
    }
}