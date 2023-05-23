using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

#nullable disable
namespace Pusula.InternManagement.Instructors
{
    public class UpdateInstructorDto
    {
        [Required]
        [StringLength(InstructorConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(InstructorConsts.MaxTitleLength)]
        public string Title { get; set; }
    }
}
