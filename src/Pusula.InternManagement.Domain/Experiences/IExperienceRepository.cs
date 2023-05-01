using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Experiences
{
    public interface IExperienceRepository : IRepository<Experience, Guid>
    {
        // Finds a experience entity by its name
        Task<Experience> FindByNameAsync(string name);

        // Gets a list of experiences entities with optional sorting, paging and cancellation.
        Task<List<Experience>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default);
    }
}
