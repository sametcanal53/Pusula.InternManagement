using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.UniversityDepartments
{
    public interface IUniversityDepartmentAppService : ICrudAppService<UniversityDepartmentDto, Guid, UniversityDepartmentGetListInput, CreateUniversityDepartmentDto, UpdateUniversityDepartmentDto>
    {
    }
}
