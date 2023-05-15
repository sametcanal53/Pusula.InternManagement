using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Permissions;
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
        private readonly IAuthorizationService _authorizationService;

        public EfCoreUniversityDepartmentRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _authorizationService = authorizationService;
        }

        public async Task<UniversityDepartment> FindByNameAsync(string name)
        {
            // Gets the DbSet<UniversityDepartment> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first University Department entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(universityDepartment => universityDepartment.Name == name);
        }

        public async Task<List<UniversityDepartment>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<UniversityDepartment> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Check if the user has admin permission for the University Departments module
            var isAdmin = await _authorizationService.IsGrantedAsync(InternManagementPermissions.UniversityDepartments.Admin);

            // Retrieve the requested page of University Department entities
            // from the database, ordered by the specified sorting criteria (or by university department name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .WhereIf(!isAdmin, universityDepartment => universityDepartment.CreatorId == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(UniversityDepartment.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
