using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.EntityFrameworkCore.Base;
using Pusula.InternManagement.Files;
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

namespace Pusula.InternManagement.EntityFrameworkCore.Files
{
    public class EfCoreFileRepository : EfCoreBaseRepository<File, Guid>, IFileRepository
    {
        public EfCoreFileRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        public async Task<File> FindByIdAndNameAsync(Guid internId, string name)
        {
            // Gets the DbSet<File> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first File entity that matches the given name and id
            return await dbSet.FirstOrDefaultAsync(file => file.Name == name && file.InternId == internId);
        }

        protected override string GetDefaultSorting()
        {
            return nameof(File.Name);
        }

        protected override string GetNameProperty(File entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Files.Admin;
        }
    }
}
