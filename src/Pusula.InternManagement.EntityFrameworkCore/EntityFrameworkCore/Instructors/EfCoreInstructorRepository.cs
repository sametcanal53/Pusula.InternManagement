using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Instructors;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Instructors
{
    public class EfCoreInstructorRepository : EfCoreRepository<InternManagementDbContext, Instructor, Guid>, IInstructorRepository
    {
        public EfCoreInstructorRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Instructor> FindByNameAsync(string name)
        {
            // Gets the DbSet<Instructor> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Instructor entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(instructor => instructor.Name == name);
        }

        public async Task<List<Instructor>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Instructor> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of Instructor entities
            // from the database, ordered by the specified sorting criteria (or by instructor name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Instructor.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
