using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Instructors
{
    public class InstructorGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
