using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.EntityFrameworkCore.Base;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Permissions;
using Pusula.InternManagement.Projects;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Projects
{
    public class EfCoreProjectRepository : EfCoreWithDetailRepository<Project, Guid, ProjectWithDetails>, IProjectRepository
    {
        public EfCoreProjectRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override async Task<IQueryable<ProjectWithDetails>> ApplyFilterAsync()
        {
            // Gets the DbContext and DbSet<Project> instances
            var dbContext = await GetDbContextAsync();
            var dbSet = await GetDbSetAsync();

            // Returns a list of ProjectWithDetails objects that includes related Intern entity
            return dbSet
                .Include(x => x.Interns)
                .Select(x => new ProjectWithDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CreatorId = x.CreatorId,
                    CreationTime = x.CreationTime,
                    LastModifierId = x.LastModifierId,
                    LastModificationTime = x.LastModificationTime,
                    IsDeleted = x.IsDeleted,
                    DeleterId = x.DeleterId,
                    DeletionTime = x.DeletionTime,
                    Interns = (from projectInterns in x.Interns
                               join intern in dbContext.Set<Intern>()
                               on projectInterns.InternId equals intern.Id
                               select $"{intern.Name} {intern.Surname}").ToList()
                });
        }

        protected override string GetDefaultSorting()
        {
            return nameof(Project.Name);
        }

        protected override Guid GetIdProperty(ProjectWithDetails entity)
        {
            return entity.Id;
        }

        protected override string GetNameProperty(Project entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Projects.Admin;
        }
    }
}