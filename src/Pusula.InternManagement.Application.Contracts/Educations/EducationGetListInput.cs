using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Educations
{
    public class EducationGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
