using Pusula.InternManagement.Interns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Works
{
    public interface IWorkAppService : ICrudAppService<WorkDto, Guid, WorkGetListInput, CreateWorkDto, UpdateWorkDto>
    {
        // GetInternLookupAsync method returns a list of interns for lookup purposes
        Task<ListResultDto<InternLookupDto>> GetInternLookupAsync();
    }
}
