using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.UniversityDepartments;
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
namespace Pusula.InternManagement.EntityFrameworkCore.UniversityDepartments
{
    public class EfCoreUniversityDepartmentRepository
        : EfCoreRepository<InternManagementDbContext, UniversityDepartment, Guid>, IUniversityDepartmentRepository
    {
        public EfCoreUniversityDepartmentRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<UniversityDepartment> FindByNameAsync(string name)
        {
            // Gets the DbSet<UniversityDepartment> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first University Department entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(universityDepartment => universityDepartment.Name == name);
        }

        public async Task<List<UniversityDepartment>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<UniversityDepartment> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of University Department entities
            // from the database, ordered by the specified sorting criteria (or by university department name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(UniversityDepartment.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
