using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

#nullable disable
namespace Pusula.InternManagement.Courses
{
    public class CourseLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }

    }
}
