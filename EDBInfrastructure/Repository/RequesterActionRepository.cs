using ESDomain.AggregateModels.RequesterActionAggregate;
using ESDomain.IRepositories;
using ESDomain.SeedWork;
using ESInfrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESInfrastructure.Repository
{
    public class RequesterActionRepository : IRequesterActionRepository
    {
        private readonly RequesterActionDbContext unitofWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitofWork;
            }
        }

        public RequesterActionRepository(RequesterActionDbContext context)
        {
            this.unitofWork = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RequesterActionDetail Add(RequesterActionDetail requesterActionDetail)
        {
            return Manage(requesterActionDetail);
        }
        public UpdateRequesterActionDetail Update(UpdateRequesterActionDetail updateRequesterActionDetail)
        {
            if (updateRequesterActionDetail != null)
            {
                this.unitofWork.updateRequesterActionDetail = updateRequesterActionDetail;
            }
            return this.unitofWork.updateRequesterActionDetail;
        }

        public RequesterActionDetail Delete(RequesterActionDetail requesterActionDetail)
        {
            return Manage(requesterActionDetail);
        }

        private RequesterActionDetail Manage(RequesterActionDetail requesterActionDetail)
        {
            this.unitofWork.requesterActionDetail = requesterActionDetail;
            return this.unitofWork.requesterActionDetail;
        }
    }
}
