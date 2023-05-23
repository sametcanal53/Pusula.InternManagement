using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

#nullable disable
namespace Pusula.InternManagement.Courses
{
    public class UpdateCourseDto
    {
        [Required]
        [StringLength(CourseConsts.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(CourseConsts.MaxDescriptionLength)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public List<string> Interns { get; set; }
        public List<string> Instructors { get; set; }

    }
}
