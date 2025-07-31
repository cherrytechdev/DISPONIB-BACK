using ESApplication.AggregateModels;
using ESDomain.AggregateModels.BusinessImageAggregate;
using ESInfrastructure.Database;
using MediatR;
using System.Data;
using System.Data.SqlClient;

namespace ESApplication.Queries.GetBusinessImages
{
    public class GetBusinessImagesDetailsQueryHandler : IRequestHandler<GetBusinessImagesDetailsQuery, List<BusinessImagesDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetBusinessImagesDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<BusinessImagesDto>> Handle(GetBusinessImagesDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<BusinessImagesDto>();
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
                            command.CommandText = "uspGetBusinessImages";
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

                        //For Landing Page
                        if (request.id == "empty")
                        {
                            response= response.GroupBy(x=> x.BusinessId)
                                .Select(group => group.OrderByDescending(item => item.CreatedOn).First())
                                .ToList();
                        }
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
        public BusinessImagesDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new BusinessImagesDto()
            {
                Id = row["id"].ToString(),
                BusinessId = row["businessid"].ToString(),
                ImageName = row["imagename"].ToString(),
                ImagePath = row["imagepath"].ToString(),
                type = Convert.ToInt16(row["type"]),
                Isactive = Convert.ToInt16(row["isactive"]),
                CreatedOn = Convert.ToDateTime(row["createdOn"])
            };
            return parentsDetail;
        }
    }
}
 