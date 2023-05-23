using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Universities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Universities
{
    public class EditModalModel : InternManagementPageModel
    {
        [BindProperty]
        public EditUniversityViewModel University { get; set; }

        private readonly IUniversityAppService _universityAppService;

        public EditModalModel(IUniversityAppService universityAppService)
        {
            _universityAppService = universityAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync(Guid id)
        {
            // Retrieves the UniversityDto object with the given id from the application service, maps it to an EditUniversityViewModel object, and assigns the result to the University property.
            var dto = await _universityAppService.GetAsync(id);
            University = ObjectMapper.Map<UniversityDto, EditUniversityViewModel>(dto);
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to update a university, passing in a UpdateUniversityDto object mapped from the University property.
            await _universityAppService.UpdateAsync(
                University.Id,
                ObjectMapper.Map<EditUniversityViewModel, UpdateUniversityDto>(University));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class EditUniversityViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(UniversityConsts.MaxNameLength)]
            public string Name { get; set; }
        }
    }
}
