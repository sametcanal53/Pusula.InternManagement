using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Files
{
    public interface IFileRepository : IRepository<File, Guid>
    {
        // Finds a file entity by its name and intern id
        Task<File> FindByIdAndNameAsync(Guid internId, string name);

        // Gets a list of files entities with optional sorting, paging and cancellation.
        Task<List<File>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default);

    }
}
