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

namespace Pusula.InternManagement.Projects
{
    [Authorize(InternManagementPermissions.Projects.Default)]
    public class ProjectAppService : InternManagementAppService, IProjectAppService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectManager _projectManager;
        private readonly IInternRepository _internRepository;

        public ProjectAppService(
            IProjectRepository projectRepository,
            ProjectManager projectManager,
            IInternRepository internRepository)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
            _internRepository = internRepository;
        }

        public async Task<PagedResultDto<ProjectDto>> GetListAsync(ProjectGetListInput input)
        {
            Log.Logger.Information($"Project list requested with sorting: {input.Sorting}, skip count: {input.SkipCount}, max result count: {input.MaxResultCount}.");

            // Get a list of project from the repository, based on the given input parameters
            var projects = await _projectRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount);

            // Count the total number of project in the repository, optionally filtering by name
            var totalCount = input.Filter == null
                ? await _projectRepository.CountAsync()
                : await _projectRepository.CountAsync(
                    project => project.Name.Contains(input.Filter));

            Log.Logger.Information($"Returning {projects.Count} projects with total count {totalCount}");

            // Map the list of project entities to a list of projectDto using the ObjectMapper
            return new PagedResultDto<ProjectDto>(
                totalCount,
                ObjectMapper.Map<List<ProjectWithDetails>, List<ProjectDto>>(projects));
        }

        public async Task<ProjectDto> GetAsync(Guid id)
        {
            // Get the project entity with the given ID from the repository
            Log.Logger.Information($"Getting project with ID {id}");
            var project = await _projectRepository.GetByIdAsync(id);

            // Map the entity to a DTO and return it
            return ObjectMapper.Map<ProjectWithDetails, ProjectDto>(project);
        }

        [Authorize(InternManagementPermissions.Projects.Create)]
        public async Task CreateAsync(CreateProjectDto input)
        {
            Log.Logger.Information($"Creating project");

            // Check if a project with the same name already exists in the repository
            var existsProject = await _projectRepository.FindByNameAsync(input.Name);
            if (existsProject != null)
            {
                Log.Logger.Error($"Cannot create a project with name {input.Name} because it already exists");
                throw new ProjectAlreadyExistsException(input.Name);
            }

            // Create a new project asynchronously using the project manager's CreateAsync method
            var project = await _projectManager.CreateAsync(
                input.Name,
                input.Description,
                input.StartDate,
                input.EndDate,
                input.Interns);

            Log.Logger.Debug($"Successfully created a new project with ID {project.Id}");

        }

        [Authorize(InternManagementPermissions.Projects.Edit)]
        public async Task UpdateAsync(Guid id, UpdateProjectDto input)
        {
            Log.Logger.Information($"Updating project with ID {id}");

            // Get the project entity with the given ID from the repository
            var project = await _projectRepository.GetAsync(id, includeDetails: true);

            // Check if another project with the same name already exists in the repository
            var existsProject = await _projectRepository.FindByNameAsync(input.Name);
            if (existsProject != null && existsProject.Id != project.Id)
            {
                Log.Logger.Error($"Cannot update the project with name {input.Name} because a project with the same name already exists");
                throw new ProjectAlreadyExistsException(input.Name);
            }

            // Update an existing project asynchronously using the project manager's UpdateAsync method
            await _projectManager.UpdateAsync(
                project,
                input.Name,
                input.Description,
                input.StartDate,
                input.EndDate,
                input.Interns);

            Log.Logger.Debug($"Project with ID {id} has been updated successfully");

        }

        [Authorize(InternManagementPermissions.Projects.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            // Delete the project entity with the given ID from the repository
            await _projectRepository.DeleteAsync(id);
            Log.Logger.Debug($"Project with ID {id} has been deleted successfully");
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
