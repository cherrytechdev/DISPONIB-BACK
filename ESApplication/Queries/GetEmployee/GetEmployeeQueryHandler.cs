using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using ESApplication.Queries.GetEmployeeDetails;

namespace ESApplication.Queries.GetUserDetails
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, List<EmployeeDetailsDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetEmployeeQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<EmployeeDetailsDto>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = new List<EmployeeDetailsDto>();
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
                            command.CommandText = "uspGETEmployeeDetails";
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

        public EmployeeDetailsDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new EmployeeDetailsDto()
            {
                empid = Convert.ToInt64(row["empid"]),
                empname = row["empname"].ToString(),
                createdon = row["createdon"].ToString()  
            };
            return parentsDetail;
        }
    }
} 