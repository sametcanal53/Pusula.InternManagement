using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Experiences
{
    public class ExperienceGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
