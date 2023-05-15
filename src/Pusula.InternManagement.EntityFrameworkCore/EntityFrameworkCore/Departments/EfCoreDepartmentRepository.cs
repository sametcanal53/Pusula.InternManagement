using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Departments;
using Pusula.InternManagement.EntityFrameworkCore;
using Pusula.InternManagement.Permissions;
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
        private readonly IAuthorizationService _authorizationService;

        public EfCoreDepartmentRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _authorizationService = authorizationService;
        }

        public async Task<Department> FindByNameAsync(string name)
        {
            // Gets the DbSet<Department> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Department entity that matches the given name
            return await dbSet
                .FirstOrDefaultAsync(department => department.Name == name);
        }

        public async Task<List<Department>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Department> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Check if the user has admin permission for the Departments module
            var isAdmin = await _authorizationService.IsGrantedAsync(InternManagementPermissions.Departments.Admin);

            // Retrieve the requested page of Department entities
            // from the database, ordered by the specified sorting criteria (or by department name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .WhereIf(!isAdmin, department => department.CreatorId == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Department.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
