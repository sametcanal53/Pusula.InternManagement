using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Projects
{
    public class ProjectGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
