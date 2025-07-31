
using ESDomain.SeedWork;
using Microsoft.AspNetCore.Http;

namespace ESDomain.AggregateModels.RequesterActionAggregate
{
    public class UpdateRequesterActionDetail : IAggregateRoot
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
 