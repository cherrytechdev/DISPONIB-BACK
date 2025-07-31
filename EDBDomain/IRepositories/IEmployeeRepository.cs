

using ESDomain.AggregateModels.EmployeeDetailAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface IEmployeeRepository : IRepository<EmployeeDetail>
    {
        EmployeeDetail Add(EmployeeDetail userDetails);
        EmployeeDetail Update(EmployeeDetail userDetails);
        EmployeeDetail Delete(EmployeeDetail userDetails);
    }
}