using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Interns;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Interns
{
    public class EfCoreInternRepository : EfCoreRepository<InternManagementDbContext, Intern, Guid>, IInternRepository
    {
        public EfCoreInternRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Intern> FindByNameAsync(string name)
        {
            // Gets the DbSet<Intern> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Intern entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(intern => intern.Name == name);
        }

        public async Task<List<Intern>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Intern> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of Intern entities
            // from the database, ordered by the specified sorting criteria (or by intern name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Intern.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
