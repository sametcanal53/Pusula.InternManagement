using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.UniversityDepartments;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.UniversityDepartments
{
    public class EditModalModel : InternManagementPageModel
    {
        [BindProperty]
        public EditUniversityDepartmentViewModel UniversityDepartment { get; set; }

        private readonly IUniversityDepartmentAppService _universityDepartmentAppService;

        public EditModalModel(IUniversityDepartmentAppService universityDepartmentAppService)
        {
            _universityDepartmentAppService = universityDepartmentAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync(Guid id)
        {
            // Retrieves the UniversityDepartmentDto object with the given id from the application service, maps it to an EditUniversityDepartmentViewModel object, and assigns the result to the University Department property.
            var dto = await _universityDepartmentAppService.GetAsync(id);
            UniversityDepartment = ObjectMapper.Map<UniversityDepartmentDto, EditUniversityDepartmentViewModel>(dto);
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to update a university department, passing in a UpdateUniversityDepartmentDto object mapped from the University Department property.
            await _universityDepartmentAppService.UpdateAsync(
                UniversityDepartment.Id,
                ObjectMapper.Map<EditUniversityDepartmentViewModel, UpdateUniversityDepartmentDto>(UniversityDepartment));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class EditUniversityDepartmentViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(UniversityDepartmentConsts.MaxNameLength)]
            public string Name { get; set; }
        }
    }
}
