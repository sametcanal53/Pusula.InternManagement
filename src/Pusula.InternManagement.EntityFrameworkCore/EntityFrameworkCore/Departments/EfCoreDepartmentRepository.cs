using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Departments;
using Pusula.InternManagement.EntityFrameworkCore;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Departments
{
    public class EfCoreDepartmentRepository : EfCoreRepository<InternManagementDbContext, Department, Guid>, IDepartmentRepository
    {
        public EfCoreDepartmentRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Department> FindByNameAsync(string name)
        {
            // Gets the DbSet<Department> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Department entity that matches the given name
            return await dbSet
                .FirstOrDefaultAsync(department => department.Name == name);
        }

        public async Task<List<Department>> GetListAsync(string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Department> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Retrieve the requested page of Department entities
            // from the database, ordered by the specified sorting criteria (or by department name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Department.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
