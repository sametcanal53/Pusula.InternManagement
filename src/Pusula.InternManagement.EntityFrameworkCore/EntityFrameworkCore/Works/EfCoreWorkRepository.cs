using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Works;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Works
{
    public class EfCoreWorkRepository : EfCoreRepository<InternManagementDbContext, Work, Guid>, IWorkRepository
    {
        public EfCoreWorkRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Work> FindByNameAsync(string name)
        {
            // Gets the DbSet<Work> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Work entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(work => work.Name == name);
        }

        public async Task<List<Work>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Work> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of Work entities
            // from the database, ordered by the specified sorting criteria (or by work name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Work.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

        }
    }
}
