using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ESApplication.Queries.GetMasterSelectDetails
{
    public class GetMasterSelectDetailsQueryHandler : IRequestHandler<GetMasterSelectDetailsQuery, List<MasterSelectDetailsDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetMasterSelectDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MasterSelectDetailsDto>> Handle(GetMasterSelectDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<MasterSelectDetailsDto>();
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
                            command.CommandText = "uspGetMasterDataDetails";
                            command.Transaction = tr;
                            command.Parameters.Add("@type", SqlDbType.Int).Value = request.type;
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = request.userid;
                            command.Parameters.Add("@val1", SqlDbType.NVarChar).Value = request.val1;
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

        public MasterSelectDetailsDto CreateFromSqlDataReader(DataRow row)
        {
            var companyDetail = new MasterSelectDetailsDto()
            { 
                code = row["code"].ToString(),  
                descriptions = row["descriptions"].ToString() 
            };
            return companyDetail;
        }
    }
} 