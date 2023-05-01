using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Experiences;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Experiences
{
    public class EfCoreExperienceRepository
        : EfCoreRepository<InternManagementDbContext, Experience, Guid>, IExperienceRepository
    {
        public EfCoreExperienceRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Experience> FindByNameAsync(string name)
        {
            // Gets the DbSet<Experience> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Experience entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(experience => experience.Name == name);
        }

        public async Task<List<Experience>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Experience> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of Experience entities
            // from the database, ordered by the specified sorting criteria (or by experience name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Experience.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
