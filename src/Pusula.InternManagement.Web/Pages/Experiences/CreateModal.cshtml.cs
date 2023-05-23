using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pusula.InternManagement.Experiences;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Pusula.InternManagement.Permissions;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Experiences
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateExperienceViewModel Experience { get; set; }

        public List<SelectListItem> Interns { get; set; }

        private readonly IExperienceAppService _experienceAppService;

        public CreateModalModel(IExperienceAppService experienceAppService)
        {
            _experienceAppService = experienceAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync()
        {
            // Initializes the Experience property with a new instance of the CreateExperienceViewModel class.
            Experience = new CreateExperienceViewModel();

            // Retrieves a list of InternLookupDto objects from the application service, maps them to a list of SelectListItem objects, and assigns the result to the Interns property.
            var internLookupDto = await _experienceAppService.GetInternLookupAsync();
            Interns = internLookupDto
                .Items
                .Select(x => new SelectListItem($"{x.Name} {x.Surname}", x.Id.ToString()))
                .ToList();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!(await AuthorizationService.IsGrantedAsync(InternManagementPermissions.Experiences.Admin)))
            {
                Experience.InternId = (Guid)CurrentUser.Id;
            }

            // Calls the application service to create a new experience, passing in a CreateExperienceDto object mapped from the Experience property.
            await _experienceAppService.CreateAsync(
                ObjectMapper.Map<CreateExperienceViewModel, CreateExperienceDto>(Experience));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class CreateExperienceViewModel
        {
            [SelectItems(nameof(Interns))]
            [DisplayName("Intern")]
            public Guid InternId { get; set; }

            [Required]
            [StringLength(ExperienceConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(ExperienceConsts.MaxTitleLength)]
            public string Title { get; set; }
            [Required]
            [StringLength(ExperienceConsts.MaxDescriptionLength)]
            [TextArea]
            public string Description { get; set; }
            [Required]
            [StringLength(ExperienceConsts.MaxCompanyNameLength)]
            public string CompanyName { get; set; }
            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime EndDate { get; set; } = DateTime.Now;
        }
    }
}
