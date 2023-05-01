using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Universities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Universities
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateUniversityViewModel University { get; set; }

        private readonly IUniversityAppService _universityAppService;

        public CreateModalModel(IUniversityAppService universityAppService)
        {
            _universityAppService = universityAppService;
        }

        // Handles the HTTP GET request for this page.
        public void OnGetAsync()
        {
            // Initializes the University property with a new instance of the CreateUniversityViewModel class.
            University = new CreateUniversityViewModel();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to create a new university, passing in a CreateUniversityDto object mapped from the University property.
            await _universityAppService.CreateAsync(
                ObjectMapper.Map<CreateUniversityViewModel, CreateUniversityDto>(University));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class CreateUniversityViewModel
        {
            [Required]
            [StringLength(UniversityConsts.MaxNameLength)]
            public string Name { get; set; }
        }
    }
}
