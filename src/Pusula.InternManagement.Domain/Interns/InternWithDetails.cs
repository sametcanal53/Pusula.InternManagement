using Pusula.InternManagement.Courses;
using Pusula.InternManagement.Educations;
using Pusula.InternManagement.Experiences;
using Pusula.InternManagement.Files;
using Pusula.InternManagement.Projects;
using Pusula.InternManagement.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

#nullable disable
namespace Pusula.InternManagement.Interns
{
    public class InternWithDetails : IFullAuditedObject
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Education> Educations { get; set; }
        public List<File> Files { get; set; }
        public List<Course> Courses { get; set; }
        public List<Experience> Experiences { get; set; }
        public List<Project> Projects { get; set; }
        public List<Work> Works { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
