using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

#nullable disable
namespace Pusula.InternManagement.Files
{
    public class FileDto : CreationAuditedEntityDto<Guid>
    {
        // Intern
        public Guid InternId { get; set; }
        public string InternName { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }
    }
}
