using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pusula.InternManagement.Instructors
{
    public class CreateInstructorDto
    {
        [Required]
        [StringLength(InstructorConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(InstructorConsts.MaxTitleLength)]
        public string Title { get; set; }

    }
}
