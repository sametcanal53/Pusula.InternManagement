using Pusula.InternManagement.Interns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Experiences
{
    public interface IExperienceAppService : ICrudAppService<ExperienceDto, Guid, ExperienceGetListInput, CreateExperienceDto, UpdateExperienceDto>
    {
        // GetInternLookupAsync method returns a list of interns for lookup purposes
        Task<ListResultDto<InternLookupDto>> GetInternLookupAsync();
    }
}
