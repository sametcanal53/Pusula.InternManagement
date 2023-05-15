using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Works
{
    public interface IWorkRepository : IRepository<Work, Guid>
    {
        // Finds a work entity by its name
        Task<Work> FindByNameAsync(string name);

        // Gets a list of works entities with optional sorting, paging and cancellation.
        Task<List<Work>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid creatorId,
            CancellationToken cancellationToken = default);

    }
}
