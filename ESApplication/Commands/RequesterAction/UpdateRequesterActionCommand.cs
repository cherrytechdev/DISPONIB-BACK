
using ESApplication.Responses;
using MediatR;


namespace ESApplication.Commands.UpdateRequesterActionDetails
{
    public class UpdateRequesterActionCommand : IRequest<Response<string>>
    {
        public Int64? referenceid { get; set; }
        public string? userid { get; set; }
        public int? statusid { get; set; }
        public string? comments { get; set; } 
        public string? immobilization { get; set; }
        public int? sitechecked { get; set; }
        public string? pcoiissuer { get; set; }
        public string? ecoiissuer { get; set; }
        public int? deisolationcheck { get; set; }
        public int? requesterunderstand { get; set; }
    }
}
