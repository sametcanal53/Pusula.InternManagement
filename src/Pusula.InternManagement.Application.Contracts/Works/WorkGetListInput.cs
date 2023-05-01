using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Works
{
    public class WorkGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
