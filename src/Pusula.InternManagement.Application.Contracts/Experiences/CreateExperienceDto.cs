using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pusula.InternManagement.Experiences
{
    public class CreateExperienceDto
    {
        //Intern
        public Guid InternId { get; set; }

        [Required]
        [StringLength(ExperienceConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ExperienceConsts.MaxTitleLength)]
        public string Title { get; set; }
        [StringLength(ExperienceConsts.MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        [StringLength(ExperienceConsts.MaxCompanyNameLength)]
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
