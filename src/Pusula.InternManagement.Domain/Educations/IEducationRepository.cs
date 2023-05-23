using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Educations
{
    public interface IEducationRepository : IRepository<Education, Guid>
    {
        // Finds a education entity by its name
        Task<Education> FindByNameAsync(string name);

        // Gets a list of educations entities with optional sorting, paging and cancellation.
        Task<List<Education>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid creatorId,
            CancellationToken cancellationToken = default);
    }
}
