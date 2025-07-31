using ESApplication.AggregateModels;
using ESInfrastructure.Database;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ESApplication.Queries.GetBusiness
{
    public class GetBusinessDetailsQueryHandler : IRequestHandler<GetBusinessDetailsQuery, List<BusinessDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetBusinessDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<BusinessDto>> Handle(GetBusinessDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<BusinessDto>();
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
                            command.CommandText = "uspGetBusinessDetails";
                            command.Transaction = tr;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = request.userid;
                            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = request.id;
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

        public BusinessDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new BusinessDto()
            {
                id = row["id"].ToString(),
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
                businessimage = row["businessimage"].ToString(),
            };
            return parentsDetail;
        }
    }
}
 