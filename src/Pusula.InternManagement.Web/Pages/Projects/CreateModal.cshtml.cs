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
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateProjectViewModel Project { get; set; }
        [BindProperty]
        public List<InternViewModel> Interns { get; set; }

        private readonly IProjectAppService _projectAppService;

        public CreateModalModel(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        // Handles the HTTP GET request for this page.
        public async void OnGetAsync()
        {
            // Initializes the Project property with a new instance of the CreateProjectViewModel class.
            Project = new CreateProjectViewModel();

            // Retrieves a list of InternLookupDto objects from the application service, maps them to a list of InternViewModel objects, and assigns the result to the Interns property.
            var internLookupDto = await _projectAppService.GetInternLookupAsync();
            Interns = ObjectMapper.Map<List<InternLookupDto>, List<InternViewModel>>(internLookupDto.Items.ToList());
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


            // Calls the application service to create a new project, passing in a CreateProjectDto object mapped from the Proje property.
            await _projectAppService.CreateAsync(
                ObjectMapper.Map<CreateProjectViewModel, CreateProjectDto>(Project));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class CreateProjectViewModel
        {
            [Required]
            [StringLength(ProjectConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(ProjectConsts.MaxDescriptionLength)]
            [TextArea]
            public string Description { get; set; }

            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; } = DateTime.Now;

            [DataType(DataType.Date)]
            public DateTime EndDate { get; set; } = DateTime.Now;

            public List<string> Interns { get; set; }
        }
    }
}
