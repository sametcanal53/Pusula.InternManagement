using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Files
{
    public class FileGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
