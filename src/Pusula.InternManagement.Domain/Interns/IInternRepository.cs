using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Interns
{
    public interface IInternRepository : IRepository<Intern, Guid>
    {
        // Finds a intern entity by its name
        Task<Intern> FindByNameAsync(string name);

        // Gets a list of interns entities with optional sorting, paging and cancellation.
        Task<List<Intern>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid creatorId,
            CancellationToken cancellationToken = default);

        Task<InternWithDetails> GetInternAsync(Guid id);
    }
}
