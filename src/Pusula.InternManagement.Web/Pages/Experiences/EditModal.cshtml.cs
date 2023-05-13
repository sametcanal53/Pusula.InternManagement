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

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Experiences
{
    public class EditModalModel : InternManagementPageModel
    {
        [BindProperty]
        public EditExperienceViewModel Experience { get; set; }

        public List<SelectListItem> Interns { get; set; }

        private readonly IExperienceAppService _experienceAppService;

        public EditModalModel(IExperienceAppService experienceAppService)
        {
            _experienceAppService = experienceAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync(Guid id)
        {
            // Retrieves the ExperienceDto object with the given id from the application service, maps it to an EditExperienceViewModel object, and assigns the result to the Experience property.
            var dto = await _experienceAppService.GetAsync(id);
            Experience = ObjectMapper.Map<ExperienceDto, EditExperienceViewModel>(dto);

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
            // Calls the application service to update a experience, passing in a UpdateExperienceDto object mapped from the Experience property.
            await _experienceAppService.UpdateAsync(
                Experience.Id,
                ObjectMapper.Map<EditExperienceViewModel, UpdateExperienceDto>(Experience));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class EditExperienceViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

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
