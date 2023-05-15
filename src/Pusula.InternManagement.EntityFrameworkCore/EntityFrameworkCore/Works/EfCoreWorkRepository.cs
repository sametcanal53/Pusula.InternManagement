using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.EntityFrameworkCore.Base;
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
    public class EfCoreWorkRepository : EfCoreBaseRepository<Work, Guid>, IWorkRepository
    {
        public EfCoreWorkRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider, 
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override string GetDefaultSorting()
        {
            return nameof(Work.Name);
        }

        protected override string GetNameProperty(Work entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Works.Admin;
        }
    }
}
