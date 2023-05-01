using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Departments;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Departments
{
    public class EditModalModel : InternManagementPageModel
    {
        [BindProperty]
        public EditDepartmentViewModel Department { get; set; }

        private readonly IDepartmentAppService _departmentAppService;

        public EditModalModel(IDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync(Guid id)
        {
            // Retrieves the DepartmentDto object with the given id from the application service, maps it to an EditDepartmentViewModel object, and assigns the result to the Department property.
            var dto = await _departmentAppService.GetAsync(id);
            Department = ObjectMapper.Map<DepartmentDto, EditDepartmentViewModel>(dto);
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to update a department, passing in a UpdateDepartmentDto object mapped from the Department property.
            await _departmentAppService.UpdateAsync(
                Department.Id,
                ObjectMapper.Map<EditDepartmentViewModel, UpdateDepartmentDto>(Department));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class EditDepartmentViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(DepartmentConsts.MaxNameLength)]
            public string Name { get; set; }
        }
    }
}
