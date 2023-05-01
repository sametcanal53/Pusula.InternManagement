using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

#nullable disable
namespace Pusula.InternManagement.EntityFrameworkCore.Files
{
    public class EfCoreFileRepository : EfCoreRepository<InternManagementDbContext, File, Guid>, IFileRepository
    {
        public EfCoreFileRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<File> FindByIdAndNameAsync(Guid internId, string name)
        {
            // Gets the DbSet<File> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first File entity that matches the given name and id
            return await dbSet.FirstOrDefaultAsync(file => file.Name == name && file.InternId == internId);
        }

        public async Task<List<File>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<File> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of File entities
            // from the database, ordered by the specified sorting criteria (or by file name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(File.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
