using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Instructors;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

namespace Pusula.InternManagement.Web.Pages.Instructors
{
    public class EditModalModel : InternManagementPageModel
    {
        [BindProperty]
        public EditInstructorViewModel Instructor { get; set; }

        private readonly IInstructorAppService _instructorAppService;

        public EditModalModel(IInstructorAppService instructorAppService)
        {
            _instructorAppService = instructorAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync(Guid id)
        {
            // Retrieves the InstructorDto object with the given id from the application service, maps it to an EditInstructorViewModel object, and assigns the result to the Instructor property.
            var dto = await _instructorAppService.GetAsync(id);
            Instructor = ObjectMapper.Map<InstructorDto, EditInstructorViewModel>(dto);
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to update a instructor, passing in a UpdateInstructorDto object mapped from the Instructor property.
            await _instructorAppService.UpdateAsync(
                Instructor.Id,
                ObjectMapper.Map<EditInstructorViewModel, UpdateInstructorDto>(Instructor));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class EditInstructorViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(InstructorConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(InstructorConsts.MaxTitleLength)]
            public string Title { get; set; }
        }
    }
}
