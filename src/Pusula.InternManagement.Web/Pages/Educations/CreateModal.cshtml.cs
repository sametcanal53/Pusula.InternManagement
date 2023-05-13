using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pusula.InternManagement.Educations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Educations
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateEducationViewModel Education { get; set; }
        public List<SelectListItem> Universities { get; set; }
        public List<SelectListItem> UniversityDepartments { get; set; }
        public List<SelectListItem> Interns { get; set; }

        private readonly IEducationAppService _educationAppService;

        public CreateModalModel(IEducationAppService educationAppService)
        {
            _educationAppService = educationAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync()
        {
            // Initializes the Education property with a new instance of the CreateEducationViewModel class.
            Education = new CreateEducationViewModel();

            // Retrieves a list of UniversityLookupDto objects from the application service, maps them to a list of SelectListItem objects, and assigns the result to the Universities property.
            var universityLookupDto = await _educationAppService.GetUniversityLookupAsync();
            Universities = universityLookupDto
                .Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            // Retrieves a list of UniversityDepartmentLookupDto objects from the application service, maps them to a list of SelectListItem objects, and assigns the result to the UniversityDepartments property.
            var universityDepartmentLookupDto = await _educationAppService.GetUniversityDepartmentLookupAsync();
            UniversityDepartments = universityDepartmentLookupDto
                .Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            // Retrieves a list of InternLookupDto objects from the application service, maps them to a list of SelectListItem objects, and assigns the result to the Interns property.
            var internLookupDto = await _educationAppService.GetInternLookupAsync();
            Interns = internLookupDto
                .Items
                .Select(x => new SelectListItem($"{x.Name} {x.Surname}", x.Id.ToString()))
                .ToList();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to create a new education, passing in a CreateEducationDto object mapped from the Education property.
            await _educationAppService.CreateAsync(
                ObjectMapper.Map<CreateEducationViewModel, CreateEducationDto>(Education));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class CreateEducationViewModel
        {
            [SelectItems(nameof(Universities))]
            [DisplayName("University")]
            public Guid UniversityId { get; set; }

            [SelectItems(nameof(UniversityDepartments))]
            [DisplayName("University Department")]
            public Guid UniversityDepartmentId { get; set; }

            [SelectItems(nameof(Interns))]
            [DisplayName("Intern")]
            public Guid InternId { get; set; }

            [Required]
            [StringLength(EducationConsts.MaxNameLength)]
            public string Name { get; set; }
            public Grade Grade { get; set; }
            public float GradePointAverage { get; set; }
            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime EndDate { get; set; } = DateTime.Now;

        }
    }
}
