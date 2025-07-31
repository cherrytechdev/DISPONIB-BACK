using ESApplication.AggregateModels;
using ESInfrastructure.Database;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ESApplication.Queries.GetPromotion
{
    public class GetPromotionDetailsQueryHandler : IRequestHandler<GetPromotionDetailsQuery, List<PromotionDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetPromotionDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<PromotionDto>> Handle(GetPromotionDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<PromotionDto>();
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
                            command.CommandText = "uspGetPromotionDetails";
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

        public PromotionDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new PromotionDto()
            {
                id = row["id"].ToString(),
                couponcode = row["couponcode"].ToString(),
                description = row["description"].ToString(),
                startdate = Convert.ToDateTime(row["startdate"]),
                enddate = Convert.ToDateTime(row["enddate"]),
                createdon = Convert.ToDateTime(row["createdon"]),
                discount = row["discount"].ToString(), 
                isactive = Convert.ToInt16(row["isactive"])
            };
            return parentsDetail;
        }
    }
}

 