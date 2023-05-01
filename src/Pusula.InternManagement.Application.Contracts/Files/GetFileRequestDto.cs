using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

#nullable disable
namespace Pusula.InternManagement.Files
{
    public class GetFileRequestDto
    {
        [Required]
        public Guid InternId { get; set; }
        [Required]
        public string Name { get; set; }

        public GetFileRequestDto(Guid internId, string name)
        {
            InternId = internId;
            Name = name;
        }
    }
}
