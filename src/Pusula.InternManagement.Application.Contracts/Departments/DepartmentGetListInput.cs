using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Departments
{
    public class DepartmentGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
