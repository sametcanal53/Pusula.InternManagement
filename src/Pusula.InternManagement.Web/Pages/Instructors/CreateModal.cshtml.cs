using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Instructors;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Instructors
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateInstructorViewModel Instructor { get; set; }

        private readonly IInstructorAppService _instructorAppService;

        public CreateModalModel(IInstructorAppService instructorAppService)
        {
            _instructorAppService = instructorAppService;
        }

        // Handles the HTTP GET request for this page.
        public void OnGetAsync()
        {
            // Initializes the Instructor property with a new instance of the CreateInstructorViewModel class.
            Instructor = new CreateInstructorViewModel();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to create a new instructor, passing in a CreateInstructorDto object mapped from the Instructor property.
            await _instructorAppService.CreateAsync(
                ObjectMapper.Map<CreateInstructorViewModel, CreateInstructorDto>(Instructor));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class CreateInstructorViewModel
        {
            [Required]
            [StringLength(InstructorConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(InstructorConsts.MaxTitleLength)]
            public string Title { get; set; }
        }
    }
}
