using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Departments;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Departments
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateDepartmentViewModel Department { get; set; }

        private readonly IDepartmentAppService _departmentAppService;

        public CreateModalModel(IDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
        }

        // Handles the HTTP GET request for this page.
        public void OnGetAsync()
        {
            // Initializes the Department property with a new instance of the CreateDepartmentViewModel class.
            Department = new CreateDepartmentViewModel();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to create a new department, passing in a CreateDepartmentDto object mapped from the Department property.
            await _departmentAppService.CreateAsync(
                ObjectMapper.Map<CreateDepartmentViewModel, CreateDepartmentDto>(Department));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class CreateDepartmentViewModel
        {
            [Required]
            [StringLength(DepartmentConsts.MaxNameLength)]
            public string Name { get; set; }
        }
    }
}
