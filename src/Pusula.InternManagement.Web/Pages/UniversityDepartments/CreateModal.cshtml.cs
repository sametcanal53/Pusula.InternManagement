using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.UniversityDepartments;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.UniversityDepartments
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateUniversityDepartmentViewModel UniversityDepartment { get; set; }

        private readonly IUniversityDepartmentAppService _universityDepartmentAppService;

        public CreateModalModel(IUniversityDepartmentAppService universityDepartmentAppService)
        {
            _universityDepartmentAppService = universityDepartmentAppService;
        }

        // Handles the HTTP GET request for this page.
        public void OnGetAsync()
        {
            // Initializes the UniversityDepartment property with a new instance of the CreateUniversityDepartmentViewModel class.
            UniversityDepartment = new CreateUniversityDepartmentViewModel();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to create a new university department, passing in a CreateUniversityDepartmentDto object mapped from the University Department property.
            await _universityDepartmentAppService.CreateAsync(
                ObjectMapper.Map<CreateUniversityDepartmentViewModel, CreateUniversityDepartmentDto>(UniversityDepartment));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }
        public class CreateUniversityDepartmentViewModel
        {
            [Required]
            [StringLength(UniversityDepartmentConsts.MaxNameLength)]
            public string Name { get; set; }
        }

    }
}
