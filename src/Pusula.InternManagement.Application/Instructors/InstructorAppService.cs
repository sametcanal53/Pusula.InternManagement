using Microsoft.AspNetCore.Authorization;
using Pusula.InternManagement.Exceptions;
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

namespace Pusula.InternManagement.Instructors
{
    [Authorize(InternManagementPermissions.Instructors.Default)]
    public class InstructorAppService : InternManagementAppService, IInstructorAppService
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly ICurrentUser _currentUser;

        public InstructorAppService(
            IInstructorRepository instructorRepository,
            ICurrentUser currentUser)
        {
            _instructorRepository = instructorRepository;
            _currentUser = currentUser;
        }
        public async Task<PagedResultDto<InstructorDto>> GetListAsync(InstructorGetListInput input)
        {
            Log.Logger.Information($"Instructor list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            // Get a list of instructors from the repository, based on the given input parameters
            var instructors = await _instructorRepository.GetListAsync(
               input.Sorting,
               input.SkipCount,
               input.MaxResultCount,
               _currentUser.GetId());

            // Count the total number of instructor in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _instructorRepository.CountAsync()
                : await _instructorRepository.CountAsync(
                    instructor => instructor.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {instructors.Count} instructors with total count {totalCount}");

            // Map the list of instructor entities to a list of instructorDto using the ObjectMapper
            return new PagedResultDto<InstructorDto>(
                totalCount,
                ObjectMapper.Map<List<Instructor>, List<InstructorDto>>(instructors));

        }
        public async Task<InstructorDto> GetAsync(Guid id)
        {
            // Get the instructor entity with the given ID from the repository
            Log.Logger.Information($"Getting instructor with ID {id}");
            var instructor = await _instructorRepository.GetAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Instructor, InstructorDto>(instructor);
        }

        [Authorize(InternManagementPermissions.Instructors.Create)]
        public async Task<InstructorDto> CreateAsync(CreateInstructorDto input)
        {
            Log.Logger.Information($"Creating instructor");
            // Check if a instructor with the same name already exists in the repository
            var existsInstructor = await _instructorRepository.FindByNameAsync(input.Name);
            if (existsInstructor != null)
            {
                Log.Logger.Error($"Cannot create a instructor with name {input.Name} because it already exists");
                throw new InstructorAlreadyExistsException(input.Name);
            }

            // Insert the new instructor entity into the repository
            var instructor = await _instructorRepository.InsertAsync(
                new Instructor(GuidGenerator.Create(), input.Name, input.Title));

            Log.Logger.Debug($"Successfully created a new instructor with ID {instructor.Id}");
            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Instructor, InstructorDto>(instructor);
        }

        [Authorize(InternManagementPermissions.Instructors.Edit)]
        public async Task<InstructorDto> UpdateAsync(Guid id, UpdateInstructorDto input)
        {
            Log.Logger.Information($"Updating instructor with ID {id}");
            // Get the instructor entity with the given ID from the repository
            var instructor = await _instructorRepository.GetAsync(id);

            // Check if another instructor with the same name already exists in the repository
            var existsInstructor = await _instructorRepository.FindByNameAsync(input.Name);
            if (existsInstructor != null && existsInstructor.Id != instructor.Id)
            {
                Log.Logger.Error($"Cannot update the instructor with name {input.Name} because a instructor with the same name already exists");
                throw new InstructorAlreadyExistsException(input.Name);
            }

            // Update the instructor entity with the input data
            instructor.SetName(input.Name);
            instructor.SetTitle(input.Title);

            // Save the changes to the instructor entity in the database
            await _instructorRepository.UpdateAsync(instructor);
            Log.Logger.Debug($"Instructor with ID {id} has been updated successfully");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Instructor, InstructorDto>(instructor);
        }

        [Authorize(InternManagementPermissions.Instructors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the instructor entity with the given ID from the repository
            await _instructorRepository.DeleteAsync(id);
            Log.Logger.Debug($"Instructor with ID {id} has been deleted successfully");
        }
    }
}
