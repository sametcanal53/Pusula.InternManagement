using Microsoft.AspNetCore.Authorization;
using Pusula.InternManagement.Exceptions;
using Pusula.InternManagement.Instructors;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Permissions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Pusula.InternManagement.Courses
{
    [Authorize(InternManagementPermissions.Courses.Default)]
    public class CourseAppService : InternManagementAppService, ICourseAppService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly CourseManager _courseManager;
        private readonly IInternRepository _internRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ICurrentUser _currentUser;

        public CourseAppService(
            ICourseRepository courseRepository,
            CourseManager courseManager,
            IInternRepository internRepository,
            IInstructorRepository instructorRepository,
            ICurrentUser currentUser)
        {
            _courseRepository = courseRepository;
            _courseManager = courseManager;
            _internRepository = internRepository;
            _instructorRepository = instructorRepository;
            _currentUser = currentUser;
        }

        public async Task<PagedResultDto<CourseDto>> GetListAsync(CourseGetListInput input)
        {
            // Get a list of courses from the repository, based on the given input parameters
            Log.Logger.Information($"Course list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            var courses = await _courseRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount,
                _currentUser.GetId());

            // Count the total number of course in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _courseRepository.CountAsync()
                : await _courseRepository.CountAsync(
                    course => course.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {courses.Count} courses with total count {totalCount}");

            // Map the list of course entities to a list of courseDto using the ObjectMapper
            return new PagedResultDto<CourseDto>(
                totalCount,
                ObjectMapper.Map<List<CourseWithDetails>, List<CourseDto>>(courses));

        }

        public async Task<CourseDto> GetAsync(Guid id)
        {
            // Get the course entity with the given ID from the repository
            Log.Logger.Information($"Getting course with ID {id}");
            var course = await _courseRepository.GetByIdAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<CourseWithDetails, CourseDto>(course);
        }

        [Authorize(InternManagementPermissions.Courses.Create)]
        public async Task CreateAsync(CreateCourseDto input)
        {
            Log.Logger.Information($"Creating course");

            // Check if a course with the same name already exists in the repository
            var existsCourse = await _courseRepository.FindByNameAsync(input.Name);
            if (existsCourse != null)
            {
                Log.Logger.Error($"Cannot create a course with name {input.Name} because it already exists");
                throw new CourseAlreadyExistsException(input.Name);
            }

            // Create a new course asynchronously using the course manager's CreateAsync method
            var course = await _courseManager.CreateAsync(
                input.Name,
                input.Description,
                input.PublishDate,
                input.Interns,
                input.Instructors);

            Log.Logger.Debug($"Successfully created a new course with ID {course.Id}");
        }

        [Authorize(InternManagementPermissions.Courses.Edit)]
        public async Task UpdateAsync(Guid id, UpdateCourseDto input)
        {
            Log.Logger.Information($"Updating course with ID {id}");

            // Get the course entity with the given ID from the repository
            var course = await _courseRepository.GetAsync(id, includeDetails: true);

            // Check if another course with the same name already exists in the repository
            var existsCourse = await _courseRepository.FindByNameAsync(input.Name);
            if (existsCourse != null && existsCourse.Id != course.Id)
            {
                Log.Logger.Error($"Cannot update the course with name {input.Name} because a course with the same name already exists");
                throw new CourseAlreadyExistsException(input.Name);
            }

            // Update an existing course asynchronously using the course manager's UpdateAsync method
            await _courseManager.UpdateAsync(
                course,
                input.Name,
                input.Description,
                input.PublishDate,
                input.Interns,
                input.Instructors);

            Log.Logger.Debug($"Course with ID {id} has been updated successfully");
        }

        [Authorize(InternManagementPermissions.Courses.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the course entity with the given ID from the repository
            await _courseRepository.DeleteAsync(id);
            Log.Logger.Debug($"Course with ID {id} has been deleted successfully");
        }

        public async Task<ListResultDto<InstructorLookupDto>> GetInstructorLookupAsync()
        {
            // Retrieve a list of instructors asynchronously using the _instructorRepository's GetListAsync method
            Log.Logger.Information("Retrieving list of instructors");
            var instructors = await _instructorRepository.GetListAsync();

            // Return a ListResultDto containing the list of mapped InstructorLookupDto objects
            return new ListResultDto<InstructorLookupDto>(
                ObjectMapper.Map<List<Instructor>, List<InstructorLookupDto>>(instructors));
        }

        public async Task<ListResultDto<InternLookupDto>> GetInternLookupAsync()
        {
            // Retrieve a list of interns asynchronously using the _internRepository's GetListAsync method
            Log.Logger.Information("Retrieving list of interns");
            var interns = await _internRepository.GetListAsync();

            // Return a ListResultDto containing the list of mapped InternLookupDto objects
            return new ListResultDto<InternLookupDto>(
                ObjectMapper.Map<List<Intern>, List<InternLookupDto>>(interns));
        }
    }
}
