using Microsoft.AspNetCore.Authorization;
using Pusula.InternManagement.Exceptions;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Permissions;
using Pusula.InternManagement.Universities;
using Pusula.InternManagement.UniversityDepartments;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Educations
{
    [Authorize(InternManagementPermissions.Educations.Default)]
    public class EducationAppService : InternManagementAppService, IEducationAppService
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IUniversityDepartmentRepository _universityDepartmentRepository;
        private readonly IInternRepository _internRepository;

        public EducationAppService(
            IEducationRepository educationRepository,
            IUniversityRepository universityRepository,
            IUniversityDepartmentRepository universityDepartmentRepository,
            IInternRepository internRepository)
        {
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _universityDepartmentRepository = universityDepartmentRepository;
            _internRepository = internRepository;
        }

        public async Task<PagedResultDto<EducationDto>> GetListAsync(EducationGetListInput input)
        {
            Log.Logger.Information($"Education list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            // Get a list of educations from the repository, based on the given input parameters
            var educations = await _educationRepository.GetListAsync(
               input.Sorting,
               input.SkipCount,
               input.MaxResultCount);

            // Get a queryable collection of university from the repository
            var universityQuery = await _universityRepository.GetQueryableAsync();

            // Get a queryable collection universityDepartment interns from the repository
            var universityDepartmentQuery = await _universityDepartmentRepository.GetQueryableAsync();

            // Get a queryable collection of interns from the repository
            var internQuery = await _internRepository.GetQueryableAsync();

            // Join the education, university, universityDepartment, and intern queries to create a new queryable object that includes data from all of the tables
            var query = (from education in educations
                         join university in universityQuery on education.UniversityId equals university.Id
                         join universityDepartment in universityDepartmentQuery on education.UniversityDepartmentId equals universityDepartment.Id
                         join intern in internQuery on education.InternId equals intern.Id
                         select new { education, university, universityDepartment, intern }).AsQueryable();

            // Execute the query and retrieve the results as a list
            var result = await AsyncExecuter.ToListAsync(query);

            // Map the query results to a list of EducationDto objects
            var dtos = result.Select(x => new EducationDto
            {
                Id = x.education.Id,
                Name = x.education.Name,
                Grade = x.education.Grade,
                GradePointAverage = x.education.GradePointAverage,
                StartDate = x.education.StartDate,
                EndDate = x.education.EndDate,
                UniversityId = x.university.Id,
                UniversityName = x.university.Name,
                UniversityDepartmentId = x.universityDepartment.Id,
                UniversityDepartmentName = x.universityDepartment.Name,
                InternId = x.intern.Id,
                InternName = x.intern.Name,
                CreationTime = x.education.CreationTime,
                CreatorId = x.education.CreatorId,
                LastModifierId = x.education.LastModifierId,
                LastModificationTime = x.education.LastModificationTime,
                IsDeleted = x.education.IsDeleted,
                DeleterId = x.education.DeleterId,
                DeletionTime = x.education.DeletionTime
            }).ToList();

            // Count the total number of department in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _educationRepository.CountAsync()
                : await _educationRepository.CountAsync(
                    education => education.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {educations.Count} educations with total count {totalCount}");

            // Return a PagedResultDto containing the total count of educations and the list of EducationDto objects
            return new PagedResultDto<EducationDto>(
                totalCount,
                dtos
            );
        }
        public async Task<EducationDto> GetAsync(Guid id)
        {
            // Get the education entity with the given ID from the repository
            Log.Logger.Information($"Getting education with ID {id}");
            var education = await _educationRepository.GetAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Education, EducationDto>(education);
        }


        [Authorize(InternManagementPermissions.Educations.Create)]
        public async Task<EducationDto> CreateAsync(CreateEducationDto input)
        {
            Log.Logger.Information($"Creating education");

            // Convert the GPA (grade point average) from a 100-point scale to a 4.0 scale, if necessary
            var gradePointAverage = input.GradePointAverage >= 4.00 ? input.GradePointAverage / 25 : input.GradePointAverage;

            // Check if a education with the same name already exists in the repository
            var existsEducation = await _educationRepository.FindByNameAsync(input.Name);
            if (existsEducation != null)
            {
                Log.Logger.Error($"Cannot create a education with name {input.Name} because it already exists");
                throw new EducationAlreadyExistsException(input.Name);
            }

            // Insert the new education entity into the repository
            var education = await _educationRepository.InsertAsync(
                new Education(GuidGenerator.Create(), input.UniversityId, input.UniversityDepartmentId, input.InternId, input.Name, input.Grade, gradePointAverage, input.StartDate, input.EndDate));

            Log.Logger.Debug($"Successfully created a new education with ID {education.Id}");
            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Education, EducationDto>(education);
        }

        [Authorize(InternManagementPermissions.Educations.Edit)]
        public async Task<EducationDto> UpdateAsync(Guid id, UpdateEducationDto input)
        {
            Log.Logger.Information($"Updating education with ID {id}");

            // Convert the GPA (grade point average) from a 100-point scale to a 4.0 scale, if necessary
            var gradePointAverage = input.GradePointAverage >= 4.00 ? input.GradePointAverage / 25 : input.GradePointAverage;

            // Get the education entity with the given ID from the repository
            var education = await _educationRepository.GetAsync(id);

            // Check if another education with the same name already exists in the repository
            var existsEducation = await _educationRepository.FindByNameAsync(input.Name);
            if (existsEducation != null && existsEducation.Id != education.Id)
            {
                Log.Logger.Error($"Cannot update the education with name {input.Name} because a education with the same name already exists");
                throw new EducationAlreadyExistsException(input.Name);
            }

            // Update the education entity with the input data
            education.SetName(input.Name);
            education.SetGrade(input.Grade);
            education.SetGradePointAverage(gradePointAverage);
            education.SetStartDate(input.StartDate);
            education.SetEndDate(input.EndDate);
            education.UniversityId = input.UniversityId;
            education.UniversityDepartmentId = input.UniversityDepartmentId;
            education.InternId = input.InternId;

            // Save the changes to the education entity in the database
            await _educationRepository.UpdateAsync(education);
            Log.Logger.Debug($"Education with ID {id} has been updated successfully");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Education, EducationDto>(education);
        }

        [Authorize(InternManagementPermissions.Educations.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the education entity with the given ID from the repository
            await _educationRepository.DeleteAsync(id);
            Log.Logger.Debug($"Education with ID {id} has been deleted successfully");
        }

        public async Task<ListResultDto<UniversityLookupDto>> GetUniversityLookupAsync()
        {
            Log.Logger.Information("Retrieving list of universities");
            // Retrieve a list of universities asynchronously using the _universityRepository's GetListAsync method
            var universities = await _universityRepository.GetListAsync();

            // Return a ListResultDto containing the list of mapped UniversityLookupDto objects
            return new ListResultDto<UniversityLookupDto>(
                ObjectMapper.Map<List<University>, List<UniversityLookupDto>>(universities));
        }

        public async Task<ListResultDto<UniversityDepartmentLookupDto>> GetUniversityDepartmentLookupAsync()
        {
            Log.Logger.Information("Retrieving list of university departments");
            // Retrieve a list of universityDepartments asynchronously using the _universityDepartmentRepository's GetListAsync method
            var universityDepartments = await _universityDepartmentRepository.GetListAsync();

            // Return a ListResultDto containing the list of mapped UniversityDepartmentLookupDto objects
            return new ListResultDto<UniversityDepartmentLookupDto>(
                ObjectMapper.Map<List<UniversityDepartment>, List<UniversityDepartmentLookupDto>>(universityDepartments));
        }

        public async Task<ListResultDto<InternLookupDto>> GetInternLookupAsync()
        {
            Log.Logger.Information("Retrieving list of interns");
            // Retrieve a list of interns asynchronously using the _internRepository's GetListAsync method
            var interns = await _internRepository.GetListAsync();

            // Return a ListResultDto containing the list of mapped InternLookupDto objects
            return new ListResultDto<InternLookupDto>(
                ObjectMapper.Map<List<Intern>, List<InternLookupDto>>(interns));
        }
    }
}
