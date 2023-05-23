using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

#nullable disable
namespace Pusula.InternManagement.UniversityDepartments
{
    public class UpdateUniversityDepartmentDto
    {
        [Required]
        [StringLength(UniversityDepartmentConsts.MaxNameLength)]
        public string Name { get; set; }

    }
}
