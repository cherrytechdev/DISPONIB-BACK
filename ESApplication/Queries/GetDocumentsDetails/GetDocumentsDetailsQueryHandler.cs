using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using ESApplication.Queries.GetDocumentsDetails; 

namespace ESApplication.Queries.GetDocumentsDetails
{
    public class GetDocumentsDetailsQueryHandler : IRequestHandler<GetDocumentsDetailsQuery, List<DocumentsDetailsDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetDocumentsDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<DocumentsDetailsDto>> Handle(GetDocumentsDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<DocumentsDetailsDto>();
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
                            command.CommandText = "uspGetDocumentsDetails";
                            command.Parameters.Add("@referenceid", SqlDbType.NVarChar).Value = request.referenceid; 
                            command.Transaction = tr;
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

        public DocumentsDetailsDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new DocumentsDetailsDto()
            { 
                Type = Convert.ToInt32(row["Type"]),   
                FileName = row["FileName"].ToString(), 
                FileUrl = row["FileUrl"].ToString(),
                FileSize = row["FileSize"].ToString(), 
                FileType = row["FileType"].ToString() 
            };
            return parentsDetail;
        }
    }
}
  