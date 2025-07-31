using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using ESApplication.Queries.GetUserDetails;

namespace ESApplication.Queries.GetCategory
{
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, List<CategoryDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCategoryDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<CategoryDto>();
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
                            command.CommandText = request.code == "0" ? "uspGetCategoryDetails" : "uspGetSubCategoryDetails";
                            command.Transaction = tr;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = request.userid;
                            if (request.code != "0")
                                command.Parameters.Add("@code", SqlDbType.NVarChar).Value = request.code;
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

        public CategoryDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new CategoryDto()
            {
                id = row["id"].ToString(),
                code = row["code"].ToString(),
                description = row["description"].ToString(),
                createdon = Convert.ToDateTime(row["createdon"]),
                isactive = Convert.ToInt16(row["isactive"])
            };
            return parentsDetail;
        }
    }
}



//public SubCategoryDetails subCategoryDetails { get; set; }