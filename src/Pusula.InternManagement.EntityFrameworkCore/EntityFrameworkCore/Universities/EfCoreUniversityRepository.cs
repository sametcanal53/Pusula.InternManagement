using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.EntityFrameworkCore.Base;
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

namespace Pusula.InternManagement.EntityFrameworkCore.Universities
{
    public class EfCoreUniversityRepository : EfCoreBaseRepository<University, Guid>, IUniversityRepository
    {
        public EfCoreUniversityRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider, 
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override string GetDefaultSorting()
        {
            return nameof(University.Name);
        }

        protected override string GetNameProperty(University entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Universities.Admin;
        }
    }
}
