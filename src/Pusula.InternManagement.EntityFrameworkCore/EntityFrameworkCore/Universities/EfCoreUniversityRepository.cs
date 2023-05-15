using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Permissions;
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
        private readonly IAuthorizationService _authorizationService;

        public EfCoreUniversityRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _authorizationService = authorizationService;
        }


        public async Task<University> FindByNameAsync(string name)
        {
            // Gets the DbSet<University> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first University entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(university => university.Name == name);
        }

        public async Task<List<University>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<University> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Check if the user has admin permission for the Universities module
            var isAdmin = await _authorizationService.IsGrantedAsync(InternManagementPermissions.Universities.Admin);

            // Retrieve the requested page of University entities
            // from the database, ordered by the specified sorting criteria (or by university name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .WhereIf(!isAdmin, university => university.CreatorId == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(University.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
