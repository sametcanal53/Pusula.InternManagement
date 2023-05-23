using Microsoft.AspNetCore.Mvc;
using Pusula.InternManagement.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.InternManagement.Controllers
{
    public class FileController : InternManagementController
    {
        private readonly IFileAppService _fileAppService;

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        // Action to download a file by name
        [HttpGet]
        [Route("api/app/file/download/{id}/{fileName}")]
        public async Task<IActionResult> DownloadAsync(Guid id, string fileName)
        {
            // Call the file app service to get the file content
            var file = await _fileAppService.GetFileAsync(new GetFileRequestDto(id, fileName));
            // Return the file content as a file result
            return File(file.Content, "application/octet-stream", file.Name);
        }
    }
}
