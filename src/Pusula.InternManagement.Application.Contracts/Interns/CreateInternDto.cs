using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;

#nullable disable
namespace Pusula.InternManagement.Interns
{
    public class CreateInternDto : IdentityUserCreateDto
    {
        public Guid DepartmentId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

    }
}
