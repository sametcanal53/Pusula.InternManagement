using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.EntityFrameworkCore.Base;
using Pusula.InternManagement.Permissions;
using Pusula.InternManagement.UniversityDepartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.InternManagement.EntityFrameworkCore.UniversityDepartments
{
    public class EfCoreUniversityDepartmentRepository : EfCoreBaseRepository<UniversityDepartment, Guid>, IUniversityDepartmentRepository
    {
        public EfCoreUniversityDepartmentRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override string GetDefaultSorting()
        {
            return nameof(UniversityDepartment.Name);
        }

        protected override string GetNameProperty(UniversityDepartment entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.UniversityDepartments.Admin;
        }
    }
}
