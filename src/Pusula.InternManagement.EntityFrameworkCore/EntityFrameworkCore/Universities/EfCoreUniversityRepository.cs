using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Universities;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Universities
{
    public class EfCoreUniversityRepository : EfCoreRepository<InternManagementDbContext, University, Guid>, IUniversityRepository
    {

        public EfCoreUniversityRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public async Task<University> FindByNameAsync(string name)
        {
            // Gets the DbSet<University> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first University entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(university => university.Name == name);
        }

        public async Task<List<University>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<University> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of University entities
            // from the database, ordered by the specified sorting criteria (or by university name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(University.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
