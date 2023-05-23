using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

#nullable disable
namespace Pusula.InternManagement.Projects
{
    public class CreateProjectDto
    {
        [Required]
        [StringLength(ProjectConsts.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(ProjectConsts.MaxDescriptionLength)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public List<string> Interns { get; set; }

    }
}
