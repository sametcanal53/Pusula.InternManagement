using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

#nullable disable
namespace Pusula.InternManagement.Instructors
{
    public class InstructorLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        
    }
}
