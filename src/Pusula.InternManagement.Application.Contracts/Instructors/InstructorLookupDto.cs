using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Instructors
{
    public class InstructorLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        
    }
}
