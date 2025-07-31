
using ESDomain.SeedWork;
using Microsoft.AspNetCore.Http;

namespace ESDomain.AggregateModels.EmployeeDetailAggregate
{
    public class EmployeeDetail : IAggregateRoot
    {
        public Int64? empid { get; set; }
        public string? empname { get; set; }
         
    }
}
