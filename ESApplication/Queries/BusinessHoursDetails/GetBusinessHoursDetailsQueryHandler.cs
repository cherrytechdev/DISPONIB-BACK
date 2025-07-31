using ESApplication.AggregateModels;
using ESInfrastructure.Database;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ESApplication.Queries.GetBusinessHours
{
    public class GetBusinessHoursDetailsQueryHandler : IRequestHandler<GetBusinessHoursDetailsQuery, List<BusinessHoursDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetBusinessHoursDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<BusinessHoursDto>> Handle(GetBusinessHoursDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<BusinessHoursDto>();
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
                            command.CommandText = "uspGetBusinessHours";
                            command.Transaction = tr; 
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

        public BusinessHoursDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new BusinessHoursDto()
            {
                id = row["id"].ToString(),
                day = row["day"].ToString(),
                starttime = Convert.ToDateTime(row["starttime"]),
                endtime = Convert.ToDateTime(row["endtime"]), 
                createdon = Convert.ToDateTime(row["createdon"]), 
                isactive = Convert.ToInt16(row["isactive"]),
                status =  row["status"].ToString()
            };
            return parentsDetail;
        }
    }
}
 