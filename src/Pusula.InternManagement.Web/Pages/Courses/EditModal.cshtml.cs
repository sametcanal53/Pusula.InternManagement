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
    public class EditModalModel : InternManagementPageModel
    {
        [BindProperty]
        public EditCourseViewModel Course { get; set; }

        [BindProperty]
        public List<InternViewModel> Interns { get; set; }

        [BindProperty]
        public List<InstructorViewModel> Instructors { get; set; }

        private readonly ICourseAppService _courseAppService;

        public EditModalModel(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync(Guid id)
        {
            // Retrieves the CourseDto object with the given id from the application service, maps it to an EditCourseViewModel object, and assigns the result to the Course property.
            var dto = await _courseAppService.GetAsync(id);
            Course = ObjectMapper.Map<CourseDto, EditCourseViewModel>(dto);

            // Retrieves a list of InternLookupDto objects from the application service, maps them to a list of InternViewModel objects, and assigns the result to the Interns property.
            var internLookupDto = await _courseAppService.GetInternLookupAsync();
            Interns = ObjectMapper.Map<List<InternLookupDto>, List<InternViewModel>>(internLookupDto.Items.ToList());

            // Retrieves a list of InstructorLookupDto objects from the application service, maps them to a list of InstructorViewModel objects, and assigns the result to the Instructors property.
            var instructors = await _courseAppService.GetInstructorLookupAsync();
            Instructors = ObjectMapper.Map<List<InstructorLookupDto>, List<InstructorViewModel>>(instructors.Items.ToList());

            // Selects the interns that are already associated with the course by setting their IsSelected property to true.
            if (Course.Interns != null && Course.Interns.Any())
            {
                Interns
                    .Where(x => Course.Interns.Contains($"{x.Name} {x.Surname}"))
                    .ToList()
                    .ForEach(x => x.IsSelected = true);
            }

            // Selects the instructors that are already associated with the course by setting their IsSelected property to true.
            if (Course.Instructors != null && Course.Instructors.Any())
            {
                Instructors
                    .Where(x => Course.Instructors.Contains(x.Name))
                    .ToList()
                    .ForEach(x => x.IsSelected = true);
            }

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

            // Calls the application service to update a course, passing in a UpdateCourseDto object mapped from the Course property.
            await _courseAppService.UpdateAsync(
                Course.Id,
                ObjectMapper.Map<EditCourseViewModel, UpdateCourseDto>(Course));

            // Returns a 204 No Content response to the client.
            return NoContent();

        }

        public class EditCourseViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(CourseConsts.MaxNameLength)]
            public string Name { get; set; }
            [Required]
            [StringLength(CourseConsts.MaxDescriptionLength)]
            [TextArea]
            public string Description { get; set; }
            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; }
            public List<string> Interns { get; set; }
            public List<string> Instructors { get; set; }
        }
    }
}
