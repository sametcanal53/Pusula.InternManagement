using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Experiences;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Experiences
{
    public class EfCoreExperienceRepository
        : EfCoreRepository<InternManagementDbContext, Experience, Guid>, IExperienceRepository
    {
        private readonly IAuthorizationService _authorizationService;

        public EfCoreExperienceRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _authorizationService = authorizationService;
        }

        public async Task<Experience> FindByNameAsync(string name)
        {
            // Gets the DbSet<Experience> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Experience entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(experience => experience.Name == name);
        }

        public async Task<List<Experience>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Experience> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Check if the user has admin permission for the Experiences module
            var isAdmin = await _authorizationService.IsGrantedAsync(InternManagementPermissions.Experiences.Admin);

            // Retrieve the requested page of Experience entities
            // from the database, ordered by the specified sorting criteria (or by experience name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .WhereIf(!isAdmin, experience => experience.CreatorId == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Experience.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
