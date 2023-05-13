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
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public CreateWorkViewModel Work { get; set; }
        public List<SelectListItem> Interns { get; set; }

        private readonly IWorkAppService _workAppService;

        public CreateModalModel(IWorkAppService workAppService)
        {
            _workAppService = workAppService;
        }

        // Handles the HTTP GET request for this page.
        public async Task OnGetAsync()
        {
            // Initializes the Work property with a new instance of the CreateWorkViewModel class.
            Work = new CreateWorkViewModel();

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
            // Calls the application service to create a new work, passing in a CreateWorkDto object mapped from the Work property.
            await _workAppService.CreateAsync(
                ObjectMapper.Map<CreateWorkViewModel, CreateWorkDto>(Work));

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class CreateWorkViewModel
        {
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
            public DateTime Date { get; set; } = DateTime.Now;
        }
    }
}
