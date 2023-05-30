using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pusula.InternManagement.Files
{
    public class SaveFileInputDto
    {
        public Guid InternId { get; set; }
        public byte[] Content { get; set; }

        [Required]
        [StringLength(FileConsts.MaxNameLength)]
        public string Name { get; set; }

        public SaveFileInputDto(Guid internId, string name, byte[] content)
        {
            InternId = internId;
            Name = name;
            Content = content;
        }
    }
}
