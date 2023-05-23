using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.InternManagement.Courses
{
    public class CourseInstructor : Entity
    {
        public Guid CourseId { get; set; }
        public Guid InstructorId { get; set; }

        public CourseInstructor(Guid courseId, Guid instructorId)
        {
            CourseId = courseId;
            InstructorId = instructorId;
        }
        public override object[] GetKeys()
        {
            return new object[] { CourseId, InstructorId };
        }
    }
}
