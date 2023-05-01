using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

#nullable disable
namespace Pusula.InternManagement.Web.Pages.Instructors
{
    public class InstructorViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        public bool IsSelected { get; set; }

        [Required]
        [HiddenInput]
        public string Name { get; set; }
    }
}
