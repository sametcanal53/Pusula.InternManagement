using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Educations;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Educations
{
    public class EfCoreEducationRepository : EfCoreRepository<InternManagementDbContext, Education, Guid>, IEducationRepository
    {
        public EfCoreEducationRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Education> FindByNameAsync(string name)
        {
            // Gets the DbSet<Education> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Education entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(education => education.Name == name);
        }

        public async Task<List<Education>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Education> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of Education entities
            // from the database, ordered by the specified sorting criteria (or by education name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Education.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
