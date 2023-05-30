using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Educations
{
    public class EducationDto : FullAuditedEntityDto<Guid>
    {
        // University
        public Guid UniversityId { get; set; }
        public string UniversityName { get; set; }

        // University Department
        public Guid UniversityDepartmentId { get; set; }
        public string UniversityDepartmentName { get; set; }

        //Intern
        public Guid InternId { get; set; }
        public string InternName { get; set; }

        public string Name { get; set; }
        public Grade Grade { get; set; }
        public float GradePointAverage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
