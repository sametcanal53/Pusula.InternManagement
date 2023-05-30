using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.Auditing;

namespace Pusula.InternManagement.Web.Pages.Interns
{
    public class InternViewModel : IMustHaveCreator
    {
        [HiddenInput]
        public Guid Id { get; set; }

        public bool IsSelected { get; set; }

        [Required]
        [HiddenInput]
        public string Name { get; set; }
        [Required]
        [HiddenInput]
        public string Surname { get; set; }

        public Guid CreatorId { get; set; }
    }
}
