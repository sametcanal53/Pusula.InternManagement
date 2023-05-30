using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Pusula.InternManagement.Interns
{
    public class UpdateInternDto : IdentityUserCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        public Guid DepartmentId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [CanBeNull]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
