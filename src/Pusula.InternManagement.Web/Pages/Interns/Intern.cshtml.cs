using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Courses;
using Pusula.InternManagement.Educations;
using Pusula.InternManagement.Experiences;
using Pusula.InternManagement.Projects;
using Pusula.InternManagement.Works;
using System.Collections.Generic;
using System;
using Pusula.InternManagement.Interns;
using System.Threading.Tasks;
using System.Linq;
using Pusula.InternManagement.Files;
using System.Text;
using System.Security.Cryptography;
using Volo.Abp.Security.Encryption;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Interns
{
    public class InternModel : InternManagementPageModel
    {
        public GetInternWithDetails Intern { get; set; }

        private readonly IInternAppService _internAppService;

        public InternModel(IInternAppService internAppService)
        {
            _internAppService = internAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var dto = await _internAppService.GetInternWithDetailsAsync(id);
            Intern = ObjectMapper.Map<InternWithDetailsDto, GetInternWithDetails>(dto);

            var departments = await _internAppService.GetDepartmentLookupAsync();
            Intern.DepartmentName = departments.Items.FirstOrDefault(x => x.Id == Intern.DepartmentId).Name;

        }

        public class GetInternWithDetails
        {
            public Guid Id { get; set; }
            public Guid DepartmentId { get; set; }
            public string DepartmentName { get; set; }
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
}
