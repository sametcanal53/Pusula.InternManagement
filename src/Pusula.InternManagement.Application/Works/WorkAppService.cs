using Microsoft.AspNetCore.Authorization;
using Pusula.InternManagement.Exceptions;
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
using static Pusula.InternManagement.Permissions.InternManagementPermissions;

namespace Pusula.InternManagement.Works
{
    [Authorize(InternManagementPermissions.Works.Default)]
    public class WorkAppService : InternManagementAppService, IWorkAppService
    {
        private readonly IWorkRepository _workRepository;
        private readonly IInternRepository _internRepository;
        private readonly ICurrentUser _currentUser;

        public WorkAppService(
            IWorkRepository workRepository,
            IInternRepository internRepository,
            ICurrentUser currentUser)
        {
            _workRepository = workRepository;
            _internRepository = internRepository;
            _currentUser = currentUser;
        }

        public async Task<PagedResultDto<WorkDto>> GetListAsync(WorkGetListInput input)
        {
            // Get a list of works from the repository, based on the given input parameters
            Log.Logger.Information($"Work list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}");
            var works = await _workRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount,
                _currentUser.GetId());

            // Get a queryable collection of interns from the repository
            var internQuery = await _internRepository.GetQueryableAsync();

            // Join the works and interns queries to create a new queryable object that includes the work and intern data
            var query = (from work in works
                         join intern in internQuery
                         on work.InternId equals intern.Id
                         select new { work, intern }).AsQueryable();

            // Execute the query and retrieve the results as a list
            var result = await AsyncExecuter.ToListAsync(query);

            // Map the query results to a list of WorkDto objects
            var dtos = result.Select(x => new WorkDto
            {
                Id = x.work.Id,
                Name = x.work.Name,
                Description = x.work.Description,
                Date = x.work.Date,
                InternId = x.intern.Id,
                InternName = $"{x.intern.Name} {x.intern.Surname}",
                CreationTime = x.work.CreationTime,
                CreatorId = x.work.CreatorId,
                LastModifierId = x.work.LastModifierId,
                LastModificationTime = x.work.LastModificationTime,
                IsDeleted = x.work.IsDeleted,
                DeleterId = x.work.DeleterId,
                DeletionTime = x.work.DeletionTime
            }).ToList();

            // Count the total number of work in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _workRepository.CountAsync()
                : await _workRepository.CountAsync(
                    work => work.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {works.Count} works with total count {totalCount}");

            // Map the list of work entities to a list of workDto using the ObjectMapper
            return new PagedResultDto<WorkDto>(
                totalCount,
                dtos
            );
        }

        public async Task<WorkDto> GetAsync(Guid id)
        {
            // Get the work entity with the given ID from the repository
            Log.Logger.Information($"Getting work with ID {id}");
            var work = await _workRepository.GetAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Work, WorkDto>(work);
        }

        [Authorize(InternManagementPermissions.Works.Create)]
        public async Task<WorkDto> CreateAsync(CreateWorkDto input)
        {
            Log.Logger.Information($"Creating work");

            // Check if the input date is outside of the intern's start and end dates
            var intern = await _internRepository.GetAsync(input.InternId);
            if (input.Date > intern.EndDate || input.Date < intern.StartDate)
            {
                throw new DateInputException();
            }

            // Insert the new work entity into the repository
            var work = await _workRepository.InsertAsync(
                new Work(
                    GuidGenerator.Create(),
                    input.InternId,
                    input.Name,
                    input.Description,
                    input.Date)
                );

            Log.Logger.Debug($"Successfully created a new work with ID {work.Id}");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Work, WorkDto>(work);
        }

        [Authorize(InternManagementPermissions.Works.Edit)]
        public async Task<WorkDto> UpdateAsync(Guid id, UpdateWorkDto input)
        {
            Log.Logger.Information($"Updating work with ID {id}");
            // Get the work entity with the given ID from the repository
            var work = await _workRepository.GetAsync(id);

            // Check if the input date is outside of the intern's start and end dates
            var intern = await _internRepository.GetAsync(input.InternId);
            if (input.Date > intern.EndDate || input.Date < intern.StartDate)
            {
                throw new DateInputException();
            }

            // Update the work entity with the input data
            work.SetName(input.Name);
            work.SetDescription(input.Description);
            work.SetDate(input.Date);
            work.InternId = input.InternId;

            // Save the changes to the work entity in the database
            await _workRepository.UpdateAsync(work);
            Log.Logger.Debug($"Work with ID {id} has been updated successfully");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Work, WorkDto>(work);
        }

        [Authorize(InternManagementPermissions.Works.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the work entity with the given ID from the repository
            await _workRepository.DeleteAsync(id);
            Log.Logger.Debug($"Work with ID {id} has been deleted successfully");
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
