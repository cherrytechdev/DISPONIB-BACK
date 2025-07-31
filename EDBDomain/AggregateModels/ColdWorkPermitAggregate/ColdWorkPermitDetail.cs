
using ESDomain.SeedWork;
using Microsoft.AspNetCore.Http;

namespace ESDomain.AggregateModels.ColdWorkPermitDetailAggregate
{
    public class ColdWorkPermitDetail : IAggregateRoot
    {
        public Int64? referenceid { get; set; }
        public string? plant { get; set; }
        public string? department { get; set; }
        public string? date { get; set; }
        public string? timefrom { get; set; }
        public string? timeto { get; set; }
        public string? equipmenttag { get; set; }
        public string? equipmentdescription { get; set; }
        public string? equipmentworklocation { get; set; }
        public string? jobdescription { get; set; }
        public string? requestorid { get; set; }
        public string? requestorname { get; set; }
        public string? requestoremail { get; set; }
        public string? requestorcontactno { get; set; }
        public string? receivertype { get; set; }
        public string? companyname { get; set; }
        public string? contactperson { get; set; }
        public string? signature { get; set; }
        public string? riskassessment { get; set; }
        public string? jobmethodstatement { get; set; }
        public string? permitissuerid { get; set; }
        public string? permitissuername { get; set; } 
        public string? permitissueremail { get; set; }
        public string? permitissuercontactno { get; set; } 
        public string? gastesting { get; set; }
        public string? continuousgastesting { get; set; }
        public string? hsdrepresentativeid { get; set; }
        public string? hsdrepresentativename { get; set; }
        public string? hsdrepresentativeemail { get; set; }
        public string? hsdrepresentativecontactno { get; set; } 
        public string? fluidpowerisolation { get; set; }
        public string? mechanicalimmobilization { get; set; }
        public string? artificiallighting { get; set; }
        public string? radiationtagno { get; set; }
        public string? workingatheight { get; set; }
        public string? equipmentisolationdetails { get; set; }
        public string? safetyhelmet { get; set; }
        public string? earplugs { get; set; }
        public string? gloves { get; set; }
        public string? faceshield { get; set; }
        public string? overallprotectiveclothing { get; set; }
        public string? respiratoryprotectiondustmask { get; set; }
        public string? breathingapparatus { get; set; }
        public string? personalgasdetector { get; set; }
        public string? safetyharness { get; set; }
        public string? fireextinguisher { get; set; }
        public string? scaffoldingladderarrangements { get; set; }
        public string? otherprotectionrequired { get; set; }
        public string? userid { get; set; }
        public int permittype { get; set; }
        public int actiontype { get; set; }
        public string? comments { get; set; }
        public string? isolationrequired { get; set; }
        public string? processisolation { get; set; }
        public string? electricalisolation { get; set; }
        public string? pcoiissuer { get; set; }
        public string? ecoiissuer { get; set; }
        public string? pcoiissuername { get; set; }
        public string? ecoiissuername { get; set; }
        public string? pcoino { get; set; }
        public string? ecoino { get; set; }
        public int? keyreceived { get; set; }
        public int? worktocommence { get; set; }
        public int? requesterunderstand { get; set; }
        public Int64? gasid { get; set; }
        public decimal? timeresult1 { get; set; }
        public decimal? timeresult2 { get; set; }
        public decimal? timeresult3 { get; set; }
        public decimal? timeresult4 { get; set; }
        public decimal? oxygenresult1 { get; set; }
        public decimal? oxygenresult2 { get; set; }
        public decimal? oxygenresult3 { get; set; }
        public decimal? oxygenresult4 { get; set; }
        public decimal? oxygenprecautions { get; set; }
        public decimal? combustible1 { get; set; }
        public decimal? combustible2 { get; set; }
        public decimal? combustible3 { get; set; }
        public decimal? combustible4 { get; set; }
        public decimal? combustibleprecautions { get; set; }
        public decimal? h2sresult1 { get; set; }
        public decimal? h2sresult2 { get; set; }
        public decimal? h2sresult3 { get; set; }
        public decimal? h2sresult4 { get; set; }
        public decimal? h2sresultprecautions { get; set; }
        public decimal? coresult1 { get; set; }
        public decimal? coresult2 { get; set; }
        public decimal? coresult3 { get; set; }
        public decimal? coresult4 { get; set; }
        public decimal? coresultprecautions { get; set; }
        public string? attachements { get; set; }
        public string? permitreceiverid { get; set; }
        public string? permitreceivercontactno { get; set; }
        public string? permitreceivername { get; set; }
        public string? permitreceiveremail { get; set; }  
    }
}
