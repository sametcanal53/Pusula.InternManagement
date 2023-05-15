using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Instructors;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Instructors
{
    public class EfCoreInstructorRepository : EfCoreRepository<InternManagementDbContext, Instructor, Guid>, IInstructorRepository
    {
        private readonly IAuthorizationService _authorizationService;

        public EfCoreInstructorRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _authorizationService = authorizationService;
        }

        public async Task<Instructor> FindByNameAsync(string name)
        {
            // Gets the DbSet<Instructor> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Instructor entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(instructor => instructor.Name == name);
        }

        public async Task<List<Instructor>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Instructor> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Check if the user has admin permission for the Instructors module
            var isAdmin = await _authorizationService.IsGrantedAsync(InternManagementPermissions.Instructors.Admin);

            // Retrieve the requested page of Instructor entities
            // from the database, ordered by the specified sorting criteria (or by instructor name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .WhereIf(!isAdmin, instructor => instructor.CreatorId == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Instructor.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
