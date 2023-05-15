using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Courses;
using Pusula.InternManagement.EntityFrameworkCore.Base;
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
    public class EfCoreCourseRepository : EfCoreWithDetailRepository<Course, Guid, CourseWithDetails>, ICourseRepository
    {
        public EfCoreCourseRepository(
            IDbContextProvider<InternManagementDbContext> dbContextProvider,
            IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override async Task<IQueryable<CourseWithDetails>> ApplyFilterAsync()
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

        protected override Guid GetCreatorId(CourseWithDetails entity)
        {
            return (Guid)entity.CreatorId;
        }

        protected override string GetDefaultSorting()
        {
            return nameof(Course.Name);
        }

        protected override Guid GetIdProperty(CourseWithDetails entity)
        {
            return entity.Id;
        }

        protected override string GetNameProperty(Course entity)
        {
            return entity.Name;
        }

        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Courses.Admin;
        }
        
    }
}
