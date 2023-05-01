using Pusula.InternManagement.Departments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Interns
{
    public interface IInternAppService : ICrudAppService<InternDto, Guid, InternGetListInput, CreateInternDto, UpdateInternDto>
    {
        // GetDepartmentLookupAsync method returns a list of departments for lookup purposes
        Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync();

    }
}
