using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Courses;
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

#nullable disable
namespace Pusula.InternManagement.EntityFrameworkCore.Base
{
    public abstract class EfCoreWithDetailRepository<TEntity, TKey, TEntityWithDetail> : EfCoreRepository<InternManagementDbContext, TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TEntityWithDetail : class
    {
        private readonly IAuthorizationService _authorizationService;

        protected EfCoreWithDetailRepository(
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

        public async Task<TEntityWithDetail> GetByIdAsync(Guid id)
        {
            var query = await ApplyFilterAsync();
            var entities = await query.ToListAsync();
            return entities.FirstOrDefault(entity => GetIdProperty(entity) == id);
        }

        public async Task<List<TEntityWithDetail>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            var query = await ApplyFilterAsync();
            var isAdmin = await _authorizationService.IsGrantedAsync(GetPermissionForModule());

            return await query
                .WhereIf(!isAdmin, entity => GetCreatorId(entity) == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : GetDefaultSorting())
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }


        protected abstract string GetPermissionForModule();
        protected abstract Task<IQueryable<TEntityWithDetail>> ApplyFilterAsync();
        protected abstract string GetNameProperty(TEntity entity);
        protected abstract Guid GetIdProperty(TEntityWithDetail entity);
        protected abstract Guid GetCreatorId(TEntityWithDetail entity);
        protected abstract string GetDefaultSorting();
    }
}
