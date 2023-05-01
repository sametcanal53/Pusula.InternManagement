using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Interns;
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
    public class EfCoreProjectRepository : EfCoreRepository<InternManagementDbContext, Project, Guid>, IProjectRepository
    {
        public EfCoreProjectRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Project> FindByNameAsync(string name)
        {
            // Gets the DbSet<Project> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Project entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(project => project.Name == name);
        }

        public async Task<ProjectWithDetails> GetByIdAsync(Guid id)
        {
            // Retrieve a filtered query of projects based on some criteria.
            var query = await ApplyFilterAsync();

            // Return the first project in the filtered query that matches the given ID.
            return await query.FirstOrDefaultAsync(project => project.Id == id);
        }

        public async Task<List<ProjectWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default)
        {
            // Applies filters to the Projects list
            var query = await ApplyFilterAsync();

            // Orders the list according to the given sorting parameter
            return await query
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Project.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        private async Task<IQueryable<ProjectWithDetails>> ApplyFilterAsync()
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
                               select intern.Name).ToList()
                });
        }

    }
}
