using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

#nullable disable
namespace Pusula.InternManagement.Educations
{
    public class UpdateEducationDto
    {
        // University
        public Guid UniversityId { get; set; }

        // University Department
        public Guid UniversityDepartmentId { get; set; }

        //Intern
        public Guid InternId { get; set; }

        [Required]
        [StringLength(EducationConsts.MaxNameLength)]
        public string Name { get; set; }
        public Grade Grade { get; set; }
        public float GradePointAverage { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

    }
}
