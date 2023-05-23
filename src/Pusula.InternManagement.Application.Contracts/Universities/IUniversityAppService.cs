using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Universities
{
    public interface IUniversityAppService : ICrudAppService<UniversityDto, Guid, UniversityGetListInput, CreateUniversityDto, UpdateUniversityDto>
    {
    }
}
