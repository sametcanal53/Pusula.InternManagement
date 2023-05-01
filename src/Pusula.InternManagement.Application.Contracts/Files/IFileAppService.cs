using Pusula.InternManagement.Interns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Files
{
    public interface IFileAppService : IApplicationService
    {
        // GetListAsync method returns a paged list of file based on the given input parameters
        Task<PagedResultDto<FileDto>> GetListAsync(FileGetListInput input);

        // GetAsync method returns a file entity with the given ID
        Task<FileDto> GetFileAsync(GetFileRequestDto input);

        // SaveFileAsync method save a new file entity with the given input data
        Task SaveFileAsync(SaveFileInputDto input);

        // DeleteAsync method deletes an existing file entity with the given ID
        Task DeleteAsync(Guid internId, string fileName);

        // GetInstructorLookupAsync method returns a list of instructors for lookup purposes
        Task<ListResultDto<InternLookupDto>> GetInternLookupAsync();
    }
}
