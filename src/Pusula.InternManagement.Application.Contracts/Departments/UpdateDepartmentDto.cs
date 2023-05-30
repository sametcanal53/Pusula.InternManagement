using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pusula.InternManagement.Departments
{
    public class UpdateDepartmentDto
    {
        [Required]
        [StringLength(DepartmentConsts.MaxNameLength)]
        public string Name { get; set; }

    }
}
