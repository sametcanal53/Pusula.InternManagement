using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Universities
{
    public class UniversityGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
