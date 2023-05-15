using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Universities
{
    public interface IUniversityRepository : IRepository<University, Guid>
    {
        // Finds a university entity by its name
        Task<University> FindByNameAsync(string name);

        // Gets a list of university entities with optional sorting, paging and cancellation.
        Task<List<University>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid creatorId,
            CancellationToken cancellationToken = default);
    }
}
