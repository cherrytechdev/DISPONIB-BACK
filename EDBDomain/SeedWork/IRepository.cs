using ESDomain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESDomain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
