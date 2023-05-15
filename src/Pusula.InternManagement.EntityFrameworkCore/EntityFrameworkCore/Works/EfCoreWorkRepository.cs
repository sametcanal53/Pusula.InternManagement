using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Permissions;
using Pusula.InternManagement.Works;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Works
{
    public class EfCoreWorkRepository : EfCoreRepository<InternManagementDbContext, Work, Guid>, IWorkRepository
    {
        private readonly IAuthorizationService _authorizationService;

        public EfCoreWorkRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _authorizationService = authorizationService;
        }

        public async Task<Work> FindByNameAsync(string name)
        {
            // Gets the DbSet<Work> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Work entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(work => work.Name == name);
        }

        public async Task<List<Work>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            // Gets the DbSet<Work> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Check if the user has admin permission for the Works module
            var isAdmin = await _authorizationService.IsGrantedAsync(InternManagementPermissions.Works.Admin);

            // Retrieve the requested page of Work entities
            // from the database, ordered by the specified sorting criteria (or by work name if sorting is not specified),
            // using the provided skip and take values, and asynchronously convert the results to a list using the cancellation token, and return the resulting list.
            return await dbSet
                .WhereIf(!isAdmin, work => work.CreatorId == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Work.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

        }
    }
}
