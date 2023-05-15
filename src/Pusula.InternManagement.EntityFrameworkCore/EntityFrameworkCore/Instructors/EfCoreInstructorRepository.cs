using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.EntityFrameworkCore.Base;
using Pusula.InternManagement.Instructors;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Instructors
{
    public class EfCoreInstructorRepository : EfCoreBaseRepository<Instructor, Guid>, IInstructorRepository
    {
        public EfCoreInstructorRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider, 
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override Guid GetCreatorId(Instructor entity)
        {
            return (Guid)entity.CreatorId;
        }

        protected override string GetDefaultSorting()
        {
            return nameof(Instructor.Name);
        }

        protected override string GetNameProperty(Instructor entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Instructors.Admin;
        }
    }
}
