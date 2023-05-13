using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pusula.InternManagement.Works;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Works
{
    public class EditModalModel : InternManagementPageModel
    {
        [BindProperty]
        public EditWorkViewModel Work { get; set; }
        public List<SelectListItem> Interns { get; set; }

        private readonly IWorkAppService _workAppService;

        public EditModalModel(IWorkAppService workAppService)
        {
            _workAppService = workAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync(Guid id)
        {
            // Retrieves the WorkDto object with the given id from the application service, maps it to an EditWorkViewModel object, and assigns the result to the Work property.
            var dto = await _workAppService.GetAsync(id);
            Work = ObjectMapper.Map<WorkDto, EditWorkViewModel>(dto);

            // Retrieves a list of InternLookupDto objects from the application service, maps them to a list of SelectListItem objects, and assigns the result to the Interns property.
            var internLookupDto = await _workAppService.GetInternLookupAsync();
            Interns = internLookupDto
                .Items
                .Select(x => new SelectListItem($"{x.Name} {x.Surname}", x.Id.ToString()))
                .ToList();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {
            // Calls the application service to update a work, passing in a UpdateWorkDto object mapped from the Work property.
            await _workAppService.UpdateAsync(
                Work.Id,
                ObjectMapper.Map<EditWorkViewModel, UpdateWorkDto>(Work));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class EditWorkViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [SelectItems(nameof(Interns))]
            [DisplayName("Intern")]
            public Guid InternId { get; set; }

            [Required]
            [StringLength(WorkConsts.MaxNameLength)]
            public string Name { get; set; }

            [Required]
            [StringLength(WorkConsts.MaxDescriptionLength)]
            [TextArea]
            public string Description { get; set; }

            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
        }
    }
}
