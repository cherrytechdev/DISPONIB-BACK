using ESApplication.AggregateModels;
using ESInfrastructure.Database;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ESApplication.Queries.GetTokenDetails
{
    public class GetTokenDetailsQueryHandler : IRequestHandler<GetTokenDetailsQuery, TokenDto>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetTokenDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<TokenDto> Handle(GetTokenDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new TokenDto();
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
                            command.CommandText = "uspGetTokenDetails";
                            command.Transaction = tr;
                            command.Parameters.Add("@token", SqlDbType.NVarChar).Value = request.token;
                           
                            DataTable dt = new DataTable();

                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                            {
                                dataAdapter.SelectCommand = command;
                                dataAdapter.Fill(dt);
                            }

                            foreach (DataRow row in dt.Rows)
                            {
                                response=CreateFromSqlDataReader(row);
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

        public TokenDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new TokenDto()
            {
                //id = Convert.ToInt64(row["id"]),
                //title = row["title"].ToString(),
                //description = row["description"].ToString(),
                //address = row["address"].ToString(),
                //categorycode = row["categorycode"].ToString(),
                //subcategorycode = row["subcategorycode"].ToString(),
                //mobile = row["mobile"].ToString(),
                //whatsapp = row["whatsapp"].ToString(),
                //email = row["email"].ToString(),
                //alternativeemail = row["alternativeemail"].ToString(),
                //website = row["website"].ToString(),
                //instagram = row["instagram"].ToString(),
                //facebook = row["facebook"].ToString(),
                //linkedin = row["linkedin"].ToString(),
                token = row["token"].ToString(),
                createdon = Convert.ToDateTime(row["creationTime"]),
               
            };
            return parentsDetail;
        }
    }
}
 