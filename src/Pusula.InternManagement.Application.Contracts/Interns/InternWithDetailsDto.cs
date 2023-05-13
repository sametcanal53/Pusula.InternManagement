using Pusula.InternManagement.Courses;
using Pusula.InternManagement.Educations;
using Pusula.InternManagement.Experiences;
using Pusula.InternManagement.Files;
using Pusula.InternManagement.Projects;
using Pusula.InternManagement.Works;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

#nullable disable
namespace Pusula.InternManagement.Interns
{
    public class InternWithDetailsDto : FullAuditedEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<EducationDto> Educations { get; set; }
        public List<FileDto> Files { get; set; }
        public List<CourseDto> Courses { get; set; }
        public List<ExperienceDto> Experiences { get; set; }
        public List<ProjectDto> Projects { get; set; }
        public List<WorkDto> Works { get; set; }
    }
}
