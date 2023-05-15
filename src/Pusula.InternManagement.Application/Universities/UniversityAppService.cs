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

namespace Pusula.InternManagement.Universities
{
    [Authorize(InternManagementPermissions.Universities.Default)]
    public class UniversityAppService : InternManagementAppService, IUniversityAppService
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly ICurrentUser _currentUser;

        public UniversityAppService(
            IUniversityRepository universityRepository,
            ICurrentUser currentUser)
        {
            _universityRepository = universityRepository;
            _currentUser = currentUser;
        }

        public async Task<PagedResultDto<UniversityDto>> GetListAsync(UniversityGetListInput input)
        {
            // Get a list of university from the repository, based on the given input parameters
            Log.Logger.Information($"University list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            var universities = await _universityRepository.GetListAsync(
               input.Sorting,
               input.SkipCount,
               input.MaxResultCount,
               _currentUser.GetId());

            // Count the total number of university in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _universityRepository.CountAsync()
                : await _universityRepository.CountAsync(
                    university => university.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {universities.Count} universities with total count {totalCount}");

            // Map the list of university entities to a list of universityDto using the ObjectMapper
            return new PagedResultDto<UniversityDto>(
                totalCount,
                ObjectMapper.Map<List<University>, List<UniversityDto>>(universities));
        }

        public async Task<UniversityDto> GetAsync(Guid id)
        {
            // Get the university entity with the given ID from the repository
            Log.Logger.Information($"Getting university with ID {id}");
            var university = await _universityRepository.GetAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<University, UniversityDto>(university);
        }

        [Authorize(InternManagementPermissions.Universities.Create)]
        public async Task<UniversityDto> CreateAsync(CreateUniversityDto input)
        {
            Log.Logger.Information($"Creating university");

            // Check if a university with the same name already exists in the repository
            var existsUniversity = await _universityRepository.FindByNameAsync(input.Name);
            if (existsUniversity != null)
            {
                Log.Logger.Error($"Cannot create a university with name {input.Name} because it already exists");
                throw new UniversityAlreadyExistsException(input.Name);
            }

            // Insert the new university entity into the repository
            var university = await _universityRepository.InsertAsync(
            new University(GuidGenerator.Create(), input.Name));

            Log.Logger.Debug($"Successfully created a new university with ID {university.Id}");
            // Map the entity to a DTO and return it
            return ObjectMapper.Map<University, UniversityDto>(university);
        }

        [Authorize(InternManagementPermissions.Universities.Edit)]
        public async Task<UniversityDto> UpdateAsync(Guid id, UpdateUniversityDto input)
        {
            Log.Logger.Information($"Updating university with ID {id}");

            // Get the university entity with the given ID from the repository
            var university = await _universityRepository.GetAsync(id);

            // Check if another university with the same name already exists in the repository
            var existsUniversity = await _universityRepository.FindByNameAsync(input.Name);
            if (existsUniversity != null && existsUniversity.Id != university.Id)
            {
                Log.Logger.Error($"Cannot update the university with name {input.Name} because a university with the same name already exists");
                throw new UniversityAlreadyExistsException(input.Name);
            }

            // Update the university entity with the input data
            university.SetName(input.Name);

            // Save the changes to the university entity in the database
            await _universityRepository.UpdateAsync(university);
            Log.Logger.Debug($"University with ID {id} has been updated successfully");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<University, UniversityDto>(university);
        }

        [Authorize(InternManagementPermissions.Universities.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the university entity with the given ID from the repository
            await _universityRepository.DeleteAsync(id);
            Log.Logger.Debug($"University with ID {id} has been deleted successfully");
        }
    }
}
