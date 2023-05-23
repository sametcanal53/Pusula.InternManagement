using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Projects
{
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        // Retrieves a project with detailed information based on its ID.
        Task<ProjectWithDetails> GetByIdAsync(Guid id);

        // Finds a project entity by its name
        Task<Project> FindByNameAsync(string name);

        // Gets a list of projects with their details based on the given input parameters
        Task<List<ProjectWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            Guid creatorId,
            CancellationToken cancellationToken = default);
    }
}
