using ESApplication.AggregateModels;
using ESApplication.Queries.GetBusiness;
using ESInfrastructure.Database;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ESApplication.Queries.GetReports
{
    public class GetReportsDetailsQueryHandler : IRequestHandler<GetReportsDetailsQuery, List<ReportsDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetReportsDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ReportsDto>> Handle(GetReportsDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<ReportsDto>();
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
                            command.CommandText = "uspGetReports";
                            command.Transaction = tr;
                            command.Parameters.Add("@code1", SqlDbType.NVarChar).Value = request.code1;
                            command.Parameters.Add("@code2", SqlDbType.NVarChar).Value = request.code2;
                            command.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = request.startdate;
                            command.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = request.enddate;
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

        public ReportsDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new ReportsDto()
            { 
                title = row["title"].ToString(),
                description = row["description"].ToString(),
                address = row["address"].ToString(),
                categorycode = row["categorycode"].ToString(),
                subcategorycode = row["subcategorycode"].ToString(),
                mobile = row["mobile"].ToString(),
                whatsapp = row["whatsapp"].ToString(),
                email = row["email"].ToString(),
                alternativeemail = row["alternativeemail"].ToString(),
                website = row["website"].ToString(),
                instagram = row["instagram"].ToString(),
                facebook = row["facebook"].ToString(),
                linkedin = row["linkedin"].ToString(),
                couponcode = row["couponcode"].ToString(), 
                createdon = Convert.ToDateTime(row["createdon"]), 
                isactive = Convert.ToInt16(row["isactive"]),
                promodesc = row["promodesc"].ToString(),
                discount = row["discount"].ToString(),
                startdate = Convert.ToDateTime(row["startdate"]),
                enddate = Convert.ToDateTime(row["enddate"]),
                categorydesc = row["categorydesc"].ToString(),
                subcategorydesc = row["subcategorydesc"].ToString(),
                likes = Convert.ToInt16(row["likes"]),
                dislikes = Convert.ToInt16(row["dislikes"]),
                ratings = Convert.ToInt16(row["ratings"]),
                views = Convert.ToInt16(row["views"]),
                reviews = Convert.ToInt16(row["reviews"]),
            };
            return parentsDetail;
        }
    }
}
 