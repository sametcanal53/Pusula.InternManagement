using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Educations;
using Pusula.InternManagement.EntityFrameworkCore.Base;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Educations
{
    public class EfCoreEducationRepository : EfCoreBaseRepository<Education, Guid>, IEducationRepository
    {
        public EfCoreEducationRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override Guid GetCreatorId(Education entity)
        {
            return (Guid)entity.CreatorId;
        }

        protected override string GetDefaultSorting()
        {
            return nameof(Education.Name);
        }

        protected override string GetNameProperty(Education entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Educations.Admin;
        }
    }
}
