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

namespace Pusula.InternManagement.Experiences
{
    [Authorize(InternManagementPermissions.Experiences.Default)]
    public class ExperienceAppService : InternManagementAppService, IExperienceAppService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IInternRepository _internRepository;

        public ExperienceAppService(
            IExperienceRepository experienceRepository,
            IInternRepository internRepository)
        {
            _experienceRepository = experienceRepository;
            _internRepository = internRepository;
        }

        public async Task<PagedResultDto<ExperienceDto>> GetListAsync(ExperienceGetListInput input)
        {
            Log.Logger.Information($"Experience list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");
            // Get a list of experiences from the repository, based on the given input parameters
            var experiences = await _experienceRepository.GetListAsync(
               input.Sorting,
               input.SkipCount,
               input.MaxResultCount);

            // Get a queryable collection of interns from the repository
            var internQuery = await _internRepository.GetQueryableAsync();

            // Join the experiences and interns queries to create a new queryable object that includes the experience and intern data
            var query = (from experience in experiences
                         join intern in internQuery
                         on experience.InternId equals intern.Id
                         select new { experience, intern }).AsQueryable();

            // Execute the query and retrieve the results as a list
            var result = await AsyncExecuter.ToListAsync(query);

            // Map the query results to a list of ExperienceDto objects
            var dtos = result.Select(x => new ExperienceDto
            {
                Id = x.experience.Id,
                Name = x.experience.Name,
                Description = x.experience.Description,
                CompanyName = x.experience.CompanyName,
                Title = x.experience.Title,
                StartDate = x.experience.StartDate,
                EndDate = x.experience.EndDate,
                InternId = x.intern.Id,
                InternName = $"{x.intern.Name} {x.intern.Surname}",
                CreationTime = x.experience.CreationTime,
                CreatorId = x.experience.CreatorId,
                LastModifierId = x.experience.LastModifierId,
                LastModificationTime = x.experience.LastModificationTime,
                IsDeleted = x.experience.IsDeleted,
                DeleterId = x.experience.DeleterId,
                DeletionTime = x.experience.DeletionTime
            }).ToList();

            // Count the total number of experience in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _experienceRepository.CountAsync()
                : await _experienceRepository.CountAsync(
                    experience => experience.Name.Contains(input.Filter));
            Log.Logger.Information($"Returning {experiences.Count} experiences with total count {totalCount}");

            // Return a PagedResultDto containing the total count of experiences and the list of ExperienceDto objects
            return new PagedResultDto<ExperienceDto>(
                totalCount,
                dtos
            );

        }
        public async Task<ExperienceDto> GetAsync(Guid id)
        {
            Log.Logger.Information($"Getting experience with ID {id}");
            // Get the experience entity with the given ID from the repository
            var experience = await _experienceRepository.GetAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Experience, ExperienceDto>(experience);
        }

        [Authorize(InternManagementPermissions.Experiences.Create)]
        public async Task<ExperienceDto> CreateAsync(CreateExperienceDto input)
        {
            Log.Logger.Information($"Creating experience");
            // Check if a experience with the same name already exists in the repository
            var existsExperience = await _experienceRepository.FindByNameAsync(input.Name);
            if (existsExperience != null)
            {
                Log.Logger.Error($"Cannot create a experience with name {input.Name} because it already exists");
                throw new ExperienceAlreadyExistsException(input.Name);
            }

            // Insert the new experience entity into the repository
            var experience = await _experienceRepository.InsertAsync(
                new Experience(GuidGenerator.Create(), input.InternId, input.Name, input.Title, input.Description, input.CompanyName, input.StartDate, input.EndDate));

            Log.Logger.Debug($"Successfully created a new experience with ID {experience.Id}");
            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Experience, ExperienceDto>(experience);
        }

        [Authorize(InternManagementPermissions.Experiences.Edit)]
        public async Task<ExperienceDto> UpdateAsync(Guid id, UpdateExperienceDto input)
        {
            Log.Logger.Information($"Updating experience with ID {id}");
            // Get the experience entity with the given ID from the repository
            var experience = await _experienceRepository.GetAsync(id);

            // Check if another experience with the same name already exists in the repository
            var existsExperience = await _experienceRepository.FindByNameAsync(input.Name);
            if (existsExperience != null && existsExperience.Id != experience.Id)
            {
                Log.Logger.Error($"Cannot update the experience with name {input.Name} because a experience with the same name already exists");
                throw new ExperienceAlreadyExistsException(input.Name);
            }

            // Update the experience entity with the input data
            experience.SetName(input.Name);
            experience.SetTitle(input.Title);
            experience.SetDescription(input.Description);
            experience.SetCompanyName(input.CompanyName);
            experience.SetStartDate(input.StartDate);
            experience.SetEndDate(input.EndDate);
            experience.InternId = input.InternId;

            // Save the changes to the experience entity in the database
            await _experienceRepository.UpdateAsync(experience);
            Log.Logger.Debug($"Experience with ID {id} has been updated successfully");

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<Experience, ExperienceDto>(experience);
        }

        [Authorize(InternManagementPermissions.Experiences.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the experience entity with the given ID from the repository
            await _experienceRepository.DeleteAsync(id);
            Log.Logger.Debug($"Experience with ID {id} has been deleted successfully");

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
