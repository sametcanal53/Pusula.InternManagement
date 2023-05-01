using Pusula.InternManagement.Interns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Projects
{
    public interface IProjectAppService : IApplicationService
    {
        // GetListAsync method returns a paged list of projects based on the given input parameters
        Task<PagedResultDto<ProjectDto>> GetListAsync(ProjectGetListInput input);

        // GetAsync method returns a project entity with the given ID
        Task<ProjectDto> GetAsync(Guid id);

        // CreateAsync method creates a new project entity with the given input data
        Task CreateAsync(CreateProjectDto input);

        // UpdateAsync method updates an existing project entity with the given input data
        Task UpdateAsync(Guid id, UpdateProjectDto input);

        // DeleteAsync method deletes an existing project entity with the given ID
        Task DeleteAsync(Guid id);

        // GetInternLookupAsync method returns a list of interns for lookup purposes
        Task<ListResultDto<InternLookupDto>> GetInternLookupAsync();
    }
}
