using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Instructors
{
    public interface IInstructorRepository : IRepository<Instructor, Guid>
    {
        // Finds a instructor entity by its name
        Task<Instructor> FindByNameAsync(string name);

        // Gets a list of instructors entities with optional sorting, paging and cancellation.
        Task<List<Instructor>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid creatorId,
            CancellationToken cancellationToken = default);
    }
}
