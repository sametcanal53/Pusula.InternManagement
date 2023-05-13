using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pusula.InternManagement.Experiences;
using Pusula.InternManagement.Files;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Files
{
    public class CreateModalModel : InternManagementPageModel
    {
        [BindProperty]
        public UploadFileViewModel UploadFile { get; set; }

        [BindProperty]
        public List<SelectListItem> Interns { get; set; }

        private readonly IFileAppService _fileAppService;

        public CreateModalModel(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        // Handles the HTTP GET request for this page.
        public async void OnGetAsync()
        {
            // Initializes the UploadFile property with a new instance of the UploadFileViewModel class.
            UploadFile = new UploadFileViewModel();

            // Retrieves a list of InternLookupDto objects from the application service, maps them to a list of SelectListItem objects, and assigns the result to the Interns property.
            var internLookupDto = await _fileAppService.GetInternLookupAsync();
            Interns = internLookupDto
                .Items
                .Select(x => new SelectListItem($"{x.Name} {x.Surname}", x.Id.ToString()))
                .ToList();
        }

        // Handles the HTTP POST request for this page.
        public async Task<IActionResult> OnPostAsync()
        {

            // Retrieves the file name from the uploaded file.
            var fileName = UploadFile.File.FileName;

            // Retrieves the intern ID associated with the uploaded file.
            var internId = UploadFile.InternId;

            // Creates a new memory stream and copies the uploaded file to it.
            using (var memoryStream = new MemoryStream())
            {
                await UploadFile.File.CopyToAsync(memoryStream);

                // Saves the file to the server using the file application service.
                await _fileAppService.SaveFileAsync(
                    new SaveFileInputDto(internId, fileName, memoryStream.ToArray()));

            }

            // Returns a 204 No Content response to the client.
            return NoContent();
        }

        public class UploadFileViewModel
        {
            [Required]
            [Display(Name = "File")]
            public IFormFile File { get; set; }

            [Display(Name = "Filename")]
            [HiddenInput]
            public string Name { get; set; }

            [SelectItems(nameof(Interns))]
            [DisplayName("Intern")]
            public Guid InternId { get; set; }

        }
    }
}
