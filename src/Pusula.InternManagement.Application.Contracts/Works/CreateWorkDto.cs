using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pusula.InternManagement.Works
{
    public class CreateWorkDto
    {
        //Intern
        public Guid InternId { get; set; }

        [Required]
        [StringLength(WorkConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(WorkConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
