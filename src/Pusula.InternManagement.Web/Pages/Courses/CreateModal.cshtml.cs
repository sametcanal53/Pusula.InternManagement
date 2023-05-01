using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pusula.InternManagement.Courses;
using Pusula.InternManagement.Instructors;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Web.Pages.Instructors;
using Pusula.InternManagement.Web.Pages.Interns;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Courses
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateCourseViewModel Course { get; set; }

        [BindProperty]
        public List<InternViewModel> Interns { get; set; }

        [BindProperty]
        public List<InstructorViewModel> Instructors { get; set; }

        private readonly ICourseAppService _courseAppService;

        public CreateModalModel(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        // Handles the HTTP GET request for this page.
        public async void OnGetAsync()
        {
            // Initializes the Course property with a new instance of the CreateCourseViewModel class.
            Course = new CreateCourseViewModel();

            // Retrieves a list of InternLookupDto objects from the application service, maps them to a list of InternViewModel objects, and assigns the result to the Interns property.
            var internLookupDto = await _courseAppService.GetInternLookupAsync();
            Interns = ObjectMapper.Map<List<InternLookupDto>, List<InternViewModel>>(internLookupDto.Items.ToList());

            // Retrieves a list of InstructorLookupDto objects from the application service, maps them to a list of InstructorViewModel objects, and assigns the result to the Instructors property.
            var instructors = await _courseAppService.GetInstructorLookupAsync();
            Instructors = ObjectMapper.Map<List<InstructorLookupDto>, List<InstructorViewModel>>(instructors.Items.ToList());
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Filters the Interns list to include only those that have been selected by the user, and maps the resulting list to a list of intern names. 
            var selectedInterns = Interns.Where(x => x.IsSelected).ToList();
            if (selectedInterns.Any())
            {
                var internNames = selectedInterns.Select(x => x.Name).ToList();
                Course.Interns = internNames;
            }

            // Filters the Instructors list to include only those that have been selected by the user, and maps the resulting list to a list of instructor names.
            var selectedInstructors = Instructors.Where(x => x.IsSelected).ToList();
            if (selectedInstructors.Any())
            {
                var instructorsNames = selectedInstructors.Select(x => x.Name).ToList();
                Course.Instructors = instructorsNames;
            }

            // Calls the application service to create a new course, passing in a CreateCourseDto object mapped from the Course property.
            await _courseAppService.CreateAsync(
                ObjectMapper.Map<CreateCourseViewModel, CreateCourseDto>(Course));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }


        public class CreateCourseViewModel
        {
            [Required]
            [StringLength(CourseConsts.MaxNameLength)]
            public string Name { get; set; }
            [Required]
            [StringLength(CourseConsts.MaxDescriptionLength)]
            [TextArea]
            public string Description { get; set; }
            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; } = DateTime.Now;
            public List<string> Interns { get; set; }
            public List<string> Instructors { get; set; }
        }
    }
}
