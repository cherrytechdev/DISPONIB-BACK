using ESApplication.AggregateModels;
using ESApplication.Queries.GetUserDetails;
using ESInfrastructure.Database;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ESApplication.Queries.GetReviewDetails
{
    public class GetReviewDetailsQueryHandler : IRequestHandler<GetReviewDetailsQuery, List<UserDetailsDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetReviewDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<UserDetailsDto>> Handle(GetReviewDetailsQuery request, CancellationToken cancellationToken)
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
                            command.CommandText = "uspGetReviewDetails";
                            command.Transaction = tr;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = request.userid;
                            command.Parameters.Add("@businessid", SqlDbType.NVarChar).Value = request.businessid;
                            command.Parameters.Add("@isactive", SqlDbType.Int).Value = request.isactive;
                            //command.Parameters.Add("@isquiz", SqlDbType.Int).Value = request.isquiz;
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
                username = row["username"].ToString(),
                comment = row["reviews"].ToString(), 
                email = row["email"].ToString(),
                mobile = row["mobile"].ToString(),
                createdon = Convert.ToDateTime(row["createdon"]),
                isactive = Convert.ToInt16(row["isactive"]),
                businessid =row["businessid"].ToString(),
                isquiz = Convert.ToInt16(row["isquiz"]),
                title =  row["title"].ToString()
            };
            return parentsDetail;
        }
    }
} 