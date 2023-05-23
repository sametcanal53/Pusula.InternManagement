using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

#nullable disable
namespace Pusula.InternManagement.Universities
{
    public class UpdateUniversityDto
    {
        [Required]
        [StringLength(UniversityConsts.MaxNameLength)]
        public string Name { get; set; }

    }
}
