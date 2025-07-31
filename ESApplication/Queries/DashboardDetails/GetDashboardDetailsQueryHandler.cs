using ESApplication.AggregateModels;
using ESInfrastructure.Database;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ESApplication.Queries.GetDashboard
{
    public class GetDashboardDetailsQueryHandler : IRequestHandler<GetDashboardDetailsQuery, List<DashboardDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetDashboardDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<DashboardDto>> Handle(GetDashboardDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<DashboardDto>();
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
                            command.CommandText = "uspGetDashboardDetails";
                            command.Transaction = tr; 
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = request.userid;
                            command.Parameters.Add("@businessid", SqlDbType.NVarChar).Value = request.businessid;
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

        public DashboardDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new DashboardDto()
            {
                usersactive = Convert.ToInt64(row["usersactive"]),
                usersinactive = Convert.ToInt64(row["usersinactive"]),
                reviewsapproved = Convert.ToInt64(row["reviewsapproved"]),
                reviewsrejected = Convert.ToInt64(row["reviewsrejected"]),
                businessactive = Convert.ToInt64(row["businessactive"]),
                businessinactive = Convert.ToInt64(row["businessinactive"]),
                reviewspending = Convert.ToInt64(row["reviewspending"]),
            };
            return parentsDetail;
        }
    }
}
 