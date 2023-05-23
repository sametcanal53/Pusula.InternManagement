using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Instructors
{
    public interface IInstructorAppService : ICrudAppService<InstructorDto, Guid, InstructorGetListInput, CreateInstructorDto, UpdateInstructorDto>
    {
    }
}
