using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.InternManagement.Courses
{
    public class CourseIntern : Entity
    {
        public Guid CourseId { get; set; }
        public Guid InternId { get; set; }

        public CourseIntern(Guid courseId, Guid internId)
        {
            CourseId = courseId;
            InternId = internId;
        }
        public override object[] GetKeys()
        {
            return new object[] { CourseId, InternId };
        }
    }
}
