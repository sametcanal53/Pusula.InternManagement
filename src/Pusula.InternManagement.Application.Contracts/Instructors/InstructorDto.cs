using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Instructors
{
    public class InstructorDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Title { get; set; }

    }
}
