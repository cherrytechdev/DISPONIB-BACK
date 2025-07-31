

using ESDomain.AggregateModels.EmployeeDetailAggregate;
using ESDomain.SeedWork;

namespace ESDomain.IRepositories
{
    public interface IColdWorkPermitRepository : IRepository<EmployeeDetail>
    {
        EmployeeDetail Add(EmployeeDetail childrenDetail);
        EmployeeDetail Update(EmployeeDetail childrenDetail);
        EmployeeDetail Delete(EmployeeDetail childrenDetail);
    }
}