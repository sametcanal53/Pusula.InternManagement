using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

#nullable disable
namespace Pusula.InternManagement.Departments
{
    public class CreateDepartmentDto
    {
        [Required]
        [StringLength(DepartmentConsts.MaxNameLength)]
        public string Name { get; set; }

    }
}
