using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Projects;
using Pusula.InternManagement.Web.Pages.Interns;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Pusula.InternManagement.Permissions;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Projects
{
    public class EditModalModel : InternManagementPageModel
    {
        [BindProperty]
        public EditProjectViewModel Project { get; set; }

        [BindProperty]
        public List<InternViewModel> Interns { get; set; }

        private readonly IProjectAppService _projectAppService;

        public EditModalModel(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync(Guid id)
        {
            // Retrieves the ProjectDto object with the given id from the application service, maps it to an EditProjectViewModel object, and assigns the result to the Project property.
            var dto = await _projectAppService.GetAsync(id);
            Project = ObjectMapper.Map<ProjectDto, EditProjectViewModel>(dto);

            // Retrieves a list of InternLookupDto objects from the application service, maps them to a list of InternViewModel objects, and assigns the result to the Interns property.
            var internLookupDto = await _projectAppService.GetInternLookupAsync();
            Interns = ObjectMapper.Map<List<InternLookupDto>, List<InternViewModel>>(internLookupDto.Items.ToList());

            // Selects the interns that are already associated with the project by setting their IsSelected property to true.
            if (Project.Interns != null && Project.Interns.Any())
            {
                Interns
                    .Where(x => Project.Interns.Contains($"{x.Name} {x.Surname}"))
                    .ToList()
                    .ForEach(x => x.IsSelected = true);
            }
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!(await AuthorizationService.IsGrantedAsync(InternManagementPermissions.Projects.Admin)))
            {
                Project.Interns = new List<string> { CurrentUser.Name };
            }
            else
            {
                // Filters the Interns list to include only those that have been selected by the user, and maps the resulting list to a list of intern names. 
                var selectedInterns = Interns.Where(x => x.IsSelected).ToList();
                if (selectedInterns.Any())
                {
                    var internNames = selectedInterns.Select(x => x.Name).ToList();
                    Project.Interns = internNames;
                }
            }

            // Calls the application service to update a project, passing in a UpdateProjectDto object mapped from the Project property.
            await _projectAppService.UpdateAsync(
                Project.Id,
                ObjectMapper.Map<EditProjectViewModel, UpdateProjectDto>(Project));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }


        public class EditProjectViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(ProjectConsts.MaxNameLength)]
            public string Name { get; set; }
            [Required]
            [StringLength(ProjectConsts.MaxDescriptionLength)]
            [TextArea]
            public string Description { get; set; }
            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; }
            [DataType(DataType.Date)]
            public DateTime EndDate { get; set; }
            public List<string> Interns { get; set; }
        }
    }
}
