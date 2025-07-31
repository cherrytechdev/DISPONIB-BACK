using AutoMapper;
using ESApplication.AggregateModels;
using ESApplication.Responses;
using ESInfrastructure.Database;
using MediatR;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Http;
using ESApplication.Queries.GetColdWorkPermitDetails;

namespace ESApplication.Queries.GetColdWorkPermitDetails
{
    public class GetColdWorkPermitDetailsQueryHandler : IRequestHandler<GetColdWorkPermitDetailsQuery, List<ColdWorkPermitDetailDto>>
    {
        public readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetColdWorkPermitDetailsQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ColdWorkPermitDetailDto>> Handle(GetColdWorkPermitDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new List<ColdWorkPermitDetailDto>();
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
                            command.CommandText = "uspGetPermitDetails";
                            command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = request.userid;
                            command.Parameters.Add("@type", SqlDbType.NVarChar).Value = request.type;
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

        public ColdWorkPermitDetailDto CreateFromSqlDataReader(DataRow row)
        {
            var parentsDetail = new ColdWorkPermitDetailDto()
            {
                referenceid = row["referenceid"].ToString(),
                id = Convert.ToInt64(row["id"]),
                plant = row["plant"].ToString(),
                department = row["department"].ToString(),
                date = Convert.ToDateTime(row["date"]),
                timefrom = Convert.ToDateTime(row["timefrom"]),
                timeto = Convert.ToDateTime(row["timeto"]),
                equipmenttag = row["equipmenttag"].ToString(),
                equipmentdescription = row["equipmentdescription"].ToString(),
                equipmentworklocation = row["equipmentworklocation"].ToString(),
                jobdescription = row["jobdescription"].ToString(),
                requestorname = row["requestorname"].ToString(),
                requestoremail = row["requestoremail"].ToString(),
                requestorcontactno = row["requestorcontactno"].ToString(),
                receivertype = row["receivertype"].ToString(),
                companyname = row["companyname"].ToString(),
                contactperson = row["contactperson"].ToString(),
                signature = row["signature"].ToString(),
                riskassessment = row["riskassessment"].ToString(),
                jobmethodstatement = row["jobmethodstatement"].ToString(), 
                permitissuerid = row["permitissuerid"].ToString(),
                permitissuername = row["permitissuername"].ToString(),
                permitissueremail = row["permitissueremail"].ToString(),
                permitissuercontactno = row["permitissuercontactno"].ToString(),
                gastesting = row["gastesting"].ToString(),
                continuousgastesting = row["continuousgastesting"].ToString(), 
                hsdrepresentativeid = row["hsdrepresentativeid"].ToString(),
                hsdrepresentativename = row["hsdrepresentativename"].ToString(),
                hsdrepresentativeemail = row["hsdrepresentativeemail"].ToString(),
                hsdrepresentativecontactno = row["hsdrepresentativecontactno"].ToString(), 
                fluidpowerisolation = row["fluidpowerisolation"].ToString(),
                mechanicalimmobilization = row["mechanicalimmobilization"].ToString(),
                artificiallighting = row["artificiallighting"].ToString(),
                radiationtagno = row["radiationtagno"].ToString(),
                workingatheight = row["workingatheight"].ToString(),
                equipmentisolationdetails = row["equipmentisolationdetails"].ToString(),
                safetyhelmet = row["safetyhelmet"].ToString(),
                earplugs = row["earplugs"].ToString(),
                gloves = row["gloves"].ToString(),
                faceshield = row["faceshield"].ToString(),
                overallprotectiveclothing = row["overallprotectiveclothing"].ToString(),
                respiratoryprotectiondustmask = row["respiratoryprotectiondustmask"].ToString(),
                breathingapparatus = row["breathingapparatus"].ToString(),
                personalgasdetector = row["personalgasdetector"].ToString(),
                safetyharness = row["safetyharness"].ToString(),
                fireextinguisher = row["fireextinguisher"].ToString(),
                scaffoldingladderarrangements = row["scaffoldingladderarrangements"].ToString(),
                otherprotectionrequired = row["otherprotectionrequired"].ToString(),
                permittype = row["permittype"].ToString(),
                createdon = Convert.ToDateTime(row["createdon"]),
                createdby = row["createdby"].ToString(),
                modifiedon = Convert.ToDateTime(row["modifiedon"]),
                modifiedby = row["modifiedby"].ToString(),
                actiontype = Convert.ToInt32(row["actiontype"]),
                statusid = Convert.ToInt32(row["statusid"]),
                descriptions = row["descriptions"].ToString(),
                gasid = Convert.ToInt64(row["gasid"]),
                timeresult1 = Convert.ToDecimal(row["timeresult1"]),
                timeresult2 = Convert.ToDecimal(row["timeresult2"]),
                timeresult3 = Convert.ToDecimal(row["timeresult3"]),
                timeresult4 = Convert.ToDecimal(row["timeresult4"]),
                oxygenresult1 = Convert.ToDecimal(row["oxygenresult1"]),
                oxygenresult2 = Convert.ToDecimal(row["oxygenresult2"]),
                oxygenresult3 = Convert.ToDecimal(row["oxygenresult3"]),
                oxygenresult4 = Convert.ToDecimal(row["oxygenresult4"]),
                oxygenprecautions = Convert.ToDecimal(row["oxygenprecautions"]),
                combustible1 = Convert.ToDecimal(row["combustible1"]),
                combustible2 = Convert.ToDecimal(row["combustible2"]),
                combustible3 = Convert.ToDecimal(row["combustible3"]),
                combustible4 = Convert.ToDecimal(row["combustible4"]),
                combustibleprecautions = Convert.ToDecimal(row["combustibleprecautions"]),
                h2sresult1 = Convert.ToDecimal(row["h2sresult1"]),
                h2sresult2 = Convert.ToDecimal(row["h2sresult2"]),
                h2sresult3 = Convert.ToDecimal(row["h2sresult3"]),
                h2sresult4 = Convert.ToDecimal(row["h2sresult4"]),
                h2sresultprecautions = Convert.ToDecimal(row["h2sresultprecautions"]),
                coresult1 = Convert.ToDecimal(row["coresult1"]),
                coresult2 = Convert.ToDecimal(row["coresult2"]),
                coresult3 = Convert.ToDecimal(row["coresult3"]),
                coresult4 = Convert.ToDecimal(row["coresult4"]),
                coresultprecautions = Convert.ToDecimal(row["coresultprecautions"]),
                isolationrequired = row["isolationrequired"].ToString(),
                processisolation = row["processisolation"].ToString(),
                electricalisolation = row["electricalisolation"].ToString(),
                pcoiissuer = row["pcoiissuer"].ToString(),
                pcoiissuername = row["pcoiissuername"].ToString(),
                ecoiissuer = row["ecoiissuer"].ToString(),
                ecoiissuername = row["ecoiissuername"].ToString(),
                pcoino = row["pcoino"].ToString(),
                ecoino = row["ecoino"].ToString(),
                keyreceived = Convert.ToInt32(row["keyreceived"]),
                worktocommence = Convert.ToInt32(row["worktocommence"]),
                requesterunderstand = Convert.ToInt32(row["requesterunderstand"]),
                permitreceiverid = row["permitreceiverid"].ToString(),
                permitreceivercontactno = row["permitreceivercontactno"].ToString(),
                permitreceivername = row["permitreceivername"].ToString(),
                permitreceiveremail = row["permitreceiveremail"].ToString(),
                handoverrequestorname = row["handoverrequestorname"].ToString(),
                handoverissuername = row["handoverissuername"].ToString(),
                currentstatus = Convert.ToInt32(row["currentstatus"])
            };
            return parentsDetail;
        }
    }
}
 