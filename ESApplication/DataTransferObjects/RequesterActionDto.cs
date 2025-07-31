using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace ESApplication.AggregateModels
{
    public class RequesterActionDto
    {
        public Int64? referenceid { get; set; }
        public string? extentionrequired { get; set; }
        public string? validitydate { get; set; }
        public string? validitytime { get; set; }
        public string? processcompleted { get; set; }
        public string? completecomments { get; set; }
        public int? housekeepingcheck { get; set; }
        public string? handoverrequired { get; set; }
        public string? handoverrequestorid { get; set; }

        public string? handoverrequestorname { get; set; }

        public string? handoverrequestoremail { get; set; }

        public string? handoverrequestorcontactno { get; set; }

        public string? handoverreceiverid { get; set; }

        public string? handoverreceivercontactno { get; set; }

        public string? handoverreceivername { get; set; }

        public string? handoverreceiveremail { get; set; }

        public string? handoverissuerid { get; set; }
        public string? handoverissuername { get; set; }

        public string? handoverissueremail { get; set; }
        public string? handoverissuercontactno { get; set; }
        public string? handoverrequestortype { get; set; }
        public string? relievingreceivertype { get; set; } 
        public string? createdby { get; set; }
        public string? requestorname { get; set; }
        public string? permitreceivername { get; set; }
        public string? permitissuername { get; set; }
        
    }
}
