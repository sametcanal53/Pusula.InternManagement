using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.InternManagement.EntityFrameworkCore.Base
{
    public abstract class EfCoreBaseRepository<TEntity, TKey> : EfCoreRepository<InternManagementDbContext, TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly IAuthorizationService _authorizationService;

        public EfCoreBaseRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _authorizationService = authorizationService;
        }


        public async Task<TEntity> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            var entities = await dbSet.ToListAsync();
            return entities.FirstOrDefault(entity => GetNameProperty(entity) == name);
        }

        public async Task<List<TEntity>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            var isAdmin = await _authorizationService.IsGrantedAsync(GetPermissionForModule());

            var query = dbSet.AsQueryable();

            return await query
                .WhereIf(!isAdmin, entity => EF.Property<Guid>(entity, "CreatorId") == creatorId || EF.Property<Guid>(entity, "InternId") == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : GetDefaultSorting())
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        protected abstract string GetPermissionForModule();
        protected abstract string GetDefaultSorting();
        protected abstract string GetNameProperty(TEntity entity);
    }
}
