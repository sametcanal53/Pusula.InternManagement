using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Courses;
using Pusula.InternManagement.Instructors;
using Pusula.InternManagement.Interns;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Courses
{
    public class EfCoreCourseRepository : EfCoreRepository<InternManagementDbContext, Course, Guid>, ICourseRepository
    {
        private readonly IAuthorizationService _authorizationService;

        public EfCoreCourseRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider)
        {
            _authorizationService = authorizationService;
        }

        public async Task<Course> FindByNameAsync(string name)
        {
            // Gets the DbSet<Course> from the DbContext
            var dbSet = await GetDbSetAsync();

            // Returns the first Course entity that matches the given name
            return await dbSet.FirstOrDefaultAsync(course => course.Name == name);
        }

        public async Task<CourseWithDetails> GetByIdAsync(Guid id)
        {
            // Retrieve a filtered query of courses based on some criteria.
            var query = await ApplyFilterAsync();

            // Return the first course in the filtered query that matches the given ID.
            return await query.FirstOrDefaultAsync(course => course.Id == id);
        }

        public async Task<List<CourseWithDetails>> GetListAsync(string sorting, int skipCount, int maxResultCount, Guid creatorId, CancellationToken cancellationToken = default)
        {
            // Applies filters to the Courses list
            var query = await ApplyFilterAsync();

            // Check if the user has admin permission for the Courses module
            var isAdmin = await _authorizationService.IsGrantedAsync(InternManagementPermissions.Courses.Admin);

            // Orders the list according to the given sorting parameter
            return await query
                .WhereIf(!isAdmin, course => course.CreatorId == creatorId)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Course.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        private async Task<IQueryable<CourseWithDetails>> ApplyFilterAsync()
        {
            // Gets the DbContext and DbSet<Course> instances
            var dbContext = await GetDbContextAsync();
            var dbSet = await GetDbSetAsync();

            // Returns a list of CourseWithDetails objects that includes related Instructor and Intern entities
            return dbSet
                .Include(x => x.Interns)
                .Include(x => x.Instructors)
                .Select(x => new CourseWithDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PublishDate = x.PublishDate,
                    CreatorId = x.CreatorId,
                    CreationTime = x.CreationTime,
                    LastModifierId = x.LastModifierId,
                    LastModificationTime = x.LastModificationTime,
                    IsDeleted = x.IsDeleted,
                    DeleterId = x.DeleterId,
                    DeletionTime = x.DeletionTime,
                    Instructors = (from courseInstructors in x.Instructors
                                   join instructor in dbContext.Set<Instructor>()
                                   on courseInstructors.InstructorId equals instructor.Id
                                   select instructor.Name).ToList(),
                    Interns = (from courseInterns in x.Interns
                               join intern in dbContext.Set<Intern>()
                               on courseInterns.InternId equals intern.Id
                               select $"{intern.Name} {intern.Surname}").ToList()
                });
        }
    }
}
