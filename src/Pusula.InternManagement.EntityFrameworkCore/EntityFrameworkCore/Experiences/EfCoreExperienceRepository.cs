using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.EntityFrameworkCore.Base;
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
    public class EfCoreExperienceRepository : EfCoreBaseRepository<Experience, Guid>, IExperienceRepository
    {
        public EfCoreExperienceRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override string GetDefaultSorting()
        {
            return nameof(Experience.Name);
        }

        protected override string GetNameProperty(Experience entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Experiences.Admin;
        }
    }
}
