using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Departments
{
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
        // Finds a department entity by its name
        Task<Department> FindByNameAsync(string name);

        // Gets a list of departments entities with optional sorting, paging and cancellation.
        Task<List<Department>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid creatorId,
            CancellationToken cancellationToken = default);
    }
}
