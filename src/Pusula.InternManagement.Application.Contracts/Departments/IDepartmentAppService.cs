using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Departments
{
    public interface IDepartmentAppService : ICrudAppService<DepartmentDto, Guid, DepartmentGetListInput, CreateDepartmentDto, UpdateDepartmentDto>
    {
    }
}
