using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pusula.InternManagement.Interns;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Interns
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateInternViewModel Intern { get; set; }

        public List<SelectListItem> Departments { get; set; }

        private readonly IInternAppService _internAppService;

        public CreateModalModel(IInternAppService internAppService)
        {
            _internAppService = internAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync()
        {
            // Initializes the Intern property with a new instance of the CreateInternViewModel class.
            Intern = new CreateInternViewModel();

            // Retrieves a list of DepartmentLookupDto objects from the application service, maps them to a list of SelectListItem objects, and assigns the result to the Departments property.
            var departmentLookupDto = await _internAppService.GetDepartmentLookupAsync();
            Departments = departmentLookupDto
                .Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            Intern.UserName = Intern.Email.Split("@")[0];

            // Calls the application service to create a new intern, passing in a CreateInternDto object mapped from the Intern property.
            await _internAppService.CreateAsync(
                ObjectMapper.Map<CreateInternViewModel, CreateInternDto>(Intern));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class CreateInternViewModel
        {
            public string UserName { get; set; }

            [Required]
            [StringLength(InternConsts.MaxNameLength)]
            public string Name { get; set; }
            [Required]
            [StringLength(InternConsts.MaxSurnameLength)]
            public string Surname { get; set; }
            [Required]
            [Phone]
            public string PhoneNumber { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [SelectItems(nameof(Departments))]
            [DisplayName("Department")]
            public Guid DepartmentId { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; } = DateTime.Now;
            [Required]
            [DataType(DataType.Date)]
            public DateTime EndDate { get; set; } = DateTime.Now;

        }
    }
}
