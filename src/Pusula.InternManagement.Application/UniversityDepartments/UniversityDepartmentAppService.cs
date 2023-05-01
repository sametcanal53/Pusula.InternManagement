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

namespace Pusula.InternManagement.UniversityDepartments
{
    [Authorize(InternManagementPermissions.UniversityDepartments.Default)]
    public class UniversityDepartmentAppService : InternManagementAppService, IUniversityDepartmentAppService
    {
        private readonly IUniversityDepartmentRepository _universityDepartmentRepository;

        public UniversityDepartmentAppService(IUniversityDepartmentRepository universityDepartmentRepository)
        {
            _universityDepartmentRepository = universityDepartmentRepository;
        }

        public async Task<PagedResultDto<UniversityDepartmentDto>> GetListAsync(UniversityDepartmentGetListInput input)
        {
            // Get a list of university departments from the repository, based on the given input parameters
            Log.Logger.Information($"University department list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            var universityDepartments = await _universityDepartmentRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount);

            // Count the total number of university departments in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _universityDepartmentRepository.CountAsync()
                : await _universityDepartmentRepository.CountAsync(
                    universityDepartment => universityDepartment.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {universityDepartments.Count} university departments with total count {totalCount}");

            // Map the list of university department entities to a list of universityDepartmentDto using the ObjectMapper
            return new PagedResultDto<UniversityDepartmentDto>(
                totalCount,
                ObjectMapper.Map<List<UniversityDepartment>, List<UniversityDepartmentDto>>(universityDepartments));
        }

        public async Task<UniversityDepartmentDto> GetAsync(Guid id)
        {
            // Get the university department entity with the given ID from the repository
            Log.Logger.Information($"Getting university department with ID {id}");
            var universityDepartment = await _universityDepartmentRepository.GetAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<UniversityDepartment, UniversityDepartmentDto>(universityDepartment);
        }

        [Authorize(InternManagementPermissions.UniversityDepartments.Create)]
        public async Task<UniversityDepartmentDto> CreateAsync(CreateUniversityDepartmentDto input)
        {
            Log.Logger.Information($"Creating university department");

            // Check if a university department with the same name already exists in the repository
            var existsUniversityDepartment = await _universityDepartmentRepository.FindByNameAsync(input.Name);
            if (existsUniversityDepartment != null)
            {
                Log.Logger.Error($"Cannot create a university department with name {input.Name} because it already exists");
                throw new UniversityDepartmentAlreadyExistsException(input.Name);
            }

            // Insert the new university department entity into the repository
            var universityDepartment = await _universityDepartmentRepository.InsertAsync(
                new UniversityDepartment(GuidGenerator.Create(), input.Name));

            Log.Logger.Debug($"Successfully created a new university department with ID {universityDepartment.Id}");
            // Map the entity to a DTO and return it
            return ObjectMapper.Map<UniversityDepartment, UniversityDepartmentDto>(universityDepartment);
        }

        [Authorize(InternManagementPermissions.UniversityDepartments.Edit)]
        public async Task<UniversityDepartmentDto> UpdateAsync(Guid id, UpdateUniversityDepartmentDto input)
        {
            Log.Logger.Information($"Updating university department with ID {id}");

            // Get the university department entity with the given ID from the repository
            var universityDepartment = await _universityDepartmentRepository.GetAsync(id);

            // Check if another university department with the same name already exists in the repository
            var existsUniversityDepartment = await _universityDepartmentRepository.FindByNameAsync(input.Name);
            if (existsUniversityDepartment != null && existsUniversityDepartment.Id != universityDepartment.Id)
            {
                Log.Logger.Error($"Cannot update the university department with name {input.Name} because a university department with the same name already exists");
                throw new UniversityDepartmentAlreadyExistsException(input.Name);
            }

            // Update the universityDepartment entity with the input data
            universityDepartment.SetName(input.Name);

            // Save the changes to the universityDepartment entity in the database
            await _universityDepartmentRepository.UpdateAsync(universityDepartment);
            Log.Logger.Debug($"University department with ID {id} has been updated successfully");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<UniversityDepartment, UniversityDepartmentDto>(universityDepartment);
        }

        [Authorize(InternManagementPermissions.UniversityDepartments.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the university department entity with the given ID from the repository
            await _universityDepartmentRepository.DeleteAsync(id);
            Log.Logger.Debug($"University department with ID {id} has been deleted successfully");
        }

    }
}
