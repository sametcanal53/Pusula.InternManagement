﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Pusula.InternManagement.Courses;
using Pusula.InternManagement.Educations;
using Pusula.InternManagement.EntityFrameworkCore.Base;
using Pusula.InternManagement.Experiences;
using Pusula.InternManagement.Files;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Permissions;
using Pusula.InternManagement.Projects;
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
namespace Pusula.InternManagement.EntityFrameworkCore.Interns
{
    public class EfCoreInternRepository : EfCoreBaseRepository<Intern, Guid>, IInternRepository
    {
        public EfCoreInternRepository(IDbContextProvider<InternManagementDbContext> dbContextProvider, IAuthorizationService authorizationService) : base(dbContextProvider, authorizationService)
        {
        }

        protected override string GetDefaultSorting()
        {
            return nameof(Intern.Name);
        }

        protected override string GetNameProperty(Intern entity)
        {
            return entity.Name;
        }
        protected override string GetPermissionForModule()
        {
            return InternManagementPermissions.Interns.Admin;
        }


        public async Task<InternWithDetails> GetInternAsync(Guid id)
        {
            // Retrieve a filtered query of projects based on some criteria.
            var query = await ApplyFilterAsync();
            var intern = await query.FirstOrDefaultAsync(x => x.Id == id);

            return intern;
        }

        public async Task<IQueryable<InternWithDetails>> ApplyFilterAsync()
        {
            // Gets the DbContext and DbSet<Project> instances
            var dbContext = await GetDbContextAsync();
            var dbSet = await GetDbSetAsync();

            // Returns a list of InternWithDetails objects that includes related Intern entity
            return dbSet
                .Select(x => new InternWithDetails
                {
                    Id = x.Id,
                    DepartmentId = x.DepartmentId,
                    Name = x.Name,
                    Surname = x.Surname,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CreatorId = x.CreatorId,
                    CreationTime = x.CreationTime,
                    LastModifierId = x.LastModifierId,
                    LastModificationTime = x.LastModificationTime,
                    IsDeleted = x.IsDeleted,
                    DeleterId = x.DeleterId,
                    DeletionTime = x.DeletionTime,
                    Educations = (from education in dbContext.Educations
                                  where education.InternId == x.Id
                                  select education).ToList(),
                    Courses = (from courseIntern in dbContext.CourseInterns
                               join course in dbContext.Courses
                               on courseIntern.CourseId equals course.Id
                               where courseIntern.InternId == x.Id
                               select course).ToList(),
                    Experiences = (from experience in dbContext.Experiences
                                   where experience.InternId == x.Id
                                   select experience).ToList(),
                    Files = (from file in dbContext.Files
                             where file.InternId == x.Id
                             select file).ToList(),
                    Projects = (from project in dbContext.Projects
                                join projectIntern in dbContext.ProjectInterns
                                on project.Id equals projectIntern.ProjectId
                                where projectIntern.InternId == x.Id
                                select project).ToList(),
                    Works = (from work in dbContext.Works
                             where work.InternId == x.Id
                             select work).ToList()
                });
        }

    }
}
