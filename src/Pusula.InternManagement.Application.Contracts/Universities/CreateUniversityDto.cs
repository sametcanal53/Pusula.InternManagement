using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pusula.InternManagement.Universities
{
    public class CreateUniversityDto
    {
        [Required]
        [StringLength(UniversityConsts.MaxNameLength)]
        public string Name { get; set; }

    }
}
