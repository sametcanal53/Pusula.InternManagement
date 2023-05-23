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

namespace Pusula.InternManagement.Departments
{
    [Authorize(InternManagementPermissions.Departments.Default)]
    public class DepartmentAppService : InternManagementAppService, IDepartmentAppService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICurrentUser _currentUser;

        public DepartmentAppService(
            IDepartmentRepository departmentRepository,
            ICurrentUser currentUser)
        {
            _departmentRepository = departmentRepository;
            _currentUser = currentUser;
        }

        public async Task<PagedResultDto<DepartmentDto>> GetListAsync(DepartmentGetListInput input)
        {
            // Get a list of departments from the repository, based on the given input parameters
            Log.Logger.Information($"Department list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            var departments = await _departmentRepository.GetListAsync(
               input.Sorting,
               input.SkipCount,
               input.MaxResultCount,
               _currentUser.GetId());

            // Count the total number of department in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _departmentRepository.CountAsync()
                : await _departmentRepository.CountAsync(
                    department => department.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {departments.Count} departments with total count {totalCount}");

            // Map the list of department entities to a list of departmentDto using the ObjectMapper
            return new PagedResultDto<DepartmentDto>(
                totalCount,
                ObjectMapper.Map<List<Department>, List<DepartmentDto>>(departments));
        }
        public async Task<DepartmentDto> GetAsync(Guid id)
        {
            // Get the department entity with the given ID from the repository
            Log.Logger.Information($"Getting department with ID {id}");
            var department = await _departmentRepository.GetAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }

        [Authorize(InternManagementPermissions.Departments.Create)]
        public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto input)
        {
            Log.Logger.Information($"Creating department");

            // Check if a department with the same name already exists in the repository
            var existsDepartment = await _departmentRepository.FindByNameAsync(input.Name);
            if (existsDepartment != null)
            {
                Log.Logger.Error($"Cannot create a department with name {input.Name} because it already exists");
                throw new DepartmentAlreadyExistsException(input.Name);
            }

            // Insert the new department entity into the repository
            var department = await _departmentRepository.InsertAsync(
                new Department(GuidGenerator.Create(), input.Name));

            Log.Logger.Debug($"Successfully created a new department with ID {department.Id}");
            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }

        [Authorize(InternManagementPermissions.Departments.Edit)]
        public async Task<DepartmentDto> UpdateAsync(Guid id, UpdateDepartmentDto input)
        {
            Log.Logger.Information($"Updating department with ID {id}");

            // Get the department entity with the given ID from the repository
            var department = await _departmentRepository.GetAsync(id);

            // Check if another department with the same name already exists in the repository
            var existsDepartment = await _departmentRepository.FindByNameAsync(input.Name);
            if (existsDepartment != null && existsDepartment.Id != department.Id)
            {
                Log.Logger.Error($"Cannot update the department with name {input.Name} because a department with the same name already exists");
                throw new DepartmentAlreadyExistsException(input.Name);
            }

            // Update the department entity with the input data
            department.SetName(input.Name);

            // Save the changes to the university entity in the database
            await _departmentRepository.UpdateAsync(department);
            Log.Logger.Debug($"Department with ID {id} has been updated successfully");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Department, DepartmentDto>(department);
        }

        [Authorize(InternManagementPermissions.Departments.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the department entity with the given ID from the repository
            await _departmentRepository.DeleteAsync(id);
            Log.Logger.Debug($"Department with ID {id} has been deleted successfully");
        }
    }
}
