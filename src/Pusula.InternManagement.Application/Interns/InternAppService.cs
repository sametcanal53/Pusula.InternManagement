using Microsoft.AspNetCore.Authorization;
using Pusula.InternManagement.Departments;
using Pusula.InternManagement.Exceptions;
using Pusula.InternManagement.Permissions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Pusula.InternManagement.Interns
{
    [Authorize(InternManagementPermissions.Interns.Default)]
    public class InternAppService : InternManagementAppService, IInternAppService
    {
        private readonly IInternRepository _internRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICurrentUser _currentUser;

        public InternAppService(
            IInternRepository internRepository,
            IDepartmentRepository departmentRepository,
            ICurrentUser currentUser)
        {
            _internRepository = internRepository;
            _departmentRepository = departmentRepository;
            _currentUser = currentUser;
        }

        public async Task<PagedResultDto<InternDto>> GetListAsync(InternGetListInput input)
        {
            Log.Logger.Information($"Intern list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            // Get a list of interns from the repository, based on the given input parameters
            var interns = await _internRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount,
                _currentUser.GetId());

            // Get a queryable collection of interns from the repository
            var internQuery = await _internRepository.GetQueryableAsync();

            // Get a queryable collection of departments from the repository
            var departmentQuery = await _departmentRepository.GetQueryableAsync();

            // Join the interns and departments queries to create a new queryable object that includes the intern and department data
            var query = (from intern in interns
                         join department in departmentQuery
                         on intern.DepartmentId equals department.Id
                         select new { intern, department }).AsQueryable();

            // Execute the query and retrieve the results as a list
            var result = await AsyncExecuter.ToListAsync(query);

            // Map the query results to a list of InternDto objects
            var dtos = result.Select(x => new InternDto
            {
                Id = x.intern.Id,
                Name = x.intern.Name,
                Surname = x.intern.Surname,
                PhoneNumber = x.intern.PhoneNumber,
                Email = x.intern.Email,
                StartDate = x.intern.StartDate,
                EndDate = x.intern.EndDate,
                CreationTime = x.intern.CreationTime,
                CreatorId = x.intern.CreatorId,
                LastModifierId = x.intern.LastModifierId,
                LastModificationTime = x.intern.LastModificationTime,
                DepartmentId = x.department.Id,
                DepartmentName = x.department.Name
            }).ToList();


            // Count the total number of intern in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _internRepository.CountAsync()
                : await _internRepository.CountAsync(
                    intern => intern.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {interns.Count} interns with total count {totalCount}");

            // Return a PagedResultDto containing the total count of interns and the list of InternDto objects
            return new PagedResultDto<InternDto>(
                totalCount,
                dtos
            );
        }
        public async Task<InternDto> GetAsync(Guid id)
        {
            // Get the intern entity with the given ID from the repository
            Log.Logger.Information($"Getting intern with ID {id}");
            var intern = await _internRepository.GetAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Intern, InternDto>(intern);
        }

        [Authorize(InternManagementPermissions.Interns.Create)]
        public async Task<InternDto> CreateAsync(CreateInternDto input)
        {
            Log.Logger.Information($"Creating intern");

            // Insert the new intern entity into the repository
            var intern = await _internRepository.InsertAsync(
                new Intern(GuidGenerator.Create(), input.DepartmentId, input.Name, input.Surname, input.PhoneNumber, input.Email, input.Password, input.StartDate, input.EndDate));

            Log.Logger.Debug($"Successfully created a new intern with ID {intern.Id}");
            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Intern, InternDto>(intern);
        }

        [Authorize(InternManagementPermissions.Interns.Edit)]
        public async Task<InternDto> UpdateAsync(Guid id, UpdateInternDto input)
        {
            Log.Logger.Information($"Updating intern with ID {id}");

            // Get the intern entity with the given ID from the repository
            var intern = await _internRepository.GetAsync(id);

            // Update the intern entity with the input data
            intern.DepartmentId = input.DepartmentId;
            intern.SetName(input.Name);
            intern.SetSurname(input.Surname);
            intern.SetPhoneNumber(input.PhoneNumber);
            intern.SetEmail(input.Email);
            intern.SetStartDate(input.StartDate);
            intern.SetEndDate(input.EndDate);
            if (!string.IsNullOrWhiteSpace(input.Password))
                intern.SetPassword(input.Password);

            // Save the changes to the intern entity in the database
            await _internRepository.UpdateAsync(intern);
            Log.Logger.Debug($"Intern with ID {id} has been updated successfully");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Intern, InternDto>(intern);
        }

        [Authorize(InternManagementPermissions.Interns.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the intern entity with the given ID from the repository
            await _internRepository.DeleteAsync(id);
            Log.Logger.Debug($"Intern with ID {id} has been deleted successfully");
        }

        public async Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync()
        {
            Log.Logger.Information("Retrieving list of departments");
            // Retrieve a list of departments asynchronously using the _departmentRepository's GetListAsync method
            var departments = await _departmentRepository.GetListAsync();

            // Return a ListResultDto containing the list of mapped DepartmentLookupDto objects
            return new ListResultDto<DepartmentLookupDto>(
                ObjectMapper.Map<List<Department>, List<DepartmentLookupDto>>(departments));
        }

        public async Task<InternWithDetailsDto> GetInternWithDetailsAsync(Guid id)
        {
            var intern = await _internRepository.GetInternAsync(id);

            if (intern == null)
            {
                // throw a custom exception indicating that the intern was not found
                throw new EntityNotFoundException("Intern not found.");
            }

            // map the Intern entity to the InternDto object using AutoMapper
            return ObjectMapper.Map<InternWithDetails, InternWithDetailsDto>(intern);

        }
    }
}
