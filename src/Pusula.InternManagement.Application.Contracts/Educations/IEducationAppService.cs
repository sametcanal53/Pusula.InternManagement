using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Universities;
using Pusula.InternManagement.UniversityDepartments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Educations
{
    public interface IEducationAppService : ICrudAppService<EducationDto, Guid, EducationGetListInput, CreateEducationDto, UpdateEducationDto>
    {
        // GetUniversityLookupAsync method returns a list of universities for lookup purposes
        Task<ListResultDto<UniversityLookupDto>> GetUniversityLookupAsync();

        // GetUniversityDepartmentLookupAsync method returns a list of university departments for lookup purposes
        Task<ListResultDto<UniversityDepartmentLookupDto>> GetUniversityDepartmentLookupAsync();

        // GetInternLookupAsync method returns a list of interns for lookup purposes
        Task<ListResultDto<InternLookupDto>> GetInternLookupAsync();

    }
}
