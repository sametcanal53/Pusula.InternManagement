using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.UniversityDepartments
{
    public interface IUniversityDepartmentRepository : IRepository<UniversityDepartment, Guid>
    {
        // Finds a university department entity by its name
        Task<UniversityDepartment> FindByNameAsync(string name);

        // Gets a list of university department entities with optional sorting, paging and cancellation.
        Task<List<UniversityDepartment>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default);

    }
}
