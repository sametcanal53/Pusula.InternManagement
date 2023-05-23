using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

#nullable disable
namespace Pusula.InternManagement.Courses
{
    public class Course : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime PublishDate { get; private set; }
        public ICollection<CourseInstructor> Instructors { get; private set; }
        public ICollection<CourseIntern> Interns { get; private set; }

        public Course() { }
        public Course(Guid id, string name, string description, DateTime publishDate) : base(id)
        {
            SetName(name);
            SetDescription(description);
            SetPublishDate(publishDate);

            Instructors = new Collection<CourseInstructor>();
            Interns = new Collection<CourseIntern>();
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), CourseConsts.MaxNameLength);
        }
        public void SetDescription(string description)
        {
            Description = Check.NotNullOrWhiteSpace(description, nameof(description), CourseConsts.MaxDescriptionLength);
        }
        public void SetPublishDate(DateTime publishDate)
        {
            PublishDate = publishDate;
        }

        // Interns
        public void AddIntern(Guid internId)
        {
            Check.NotNull(internId, nameof(internId));

            if (IsInIntern(internId))
            {
                return;
            }

            Interns.Add(new CourseIntern(courseId: Id, internId));
        }

        public void RemoveIntern(Guid internId)
        {
            Check.NotNull(internId, nameof(internId));

            if (!IsInIntern(internId))
            {
                return;
            }

            Interns.RemoveAll(x => x.InternId == internId);
        }

        public void RemoveAllInternsExceptGivenIds(List<Guid> internIds)
        {
            Check.NotNullOrEmpty(internIds, nameof(internIds));

            Interns.RemoveAll(x => !internIds.Contains(x.InternId));
        }

        public void RemoveAllInterns()
        {
            Interns.RemoveAll(x => x.CourseId == Id);
        }

        private bool IsInIntern(Guid internId)
        {
            return Interns.Any(x => x.InternId == internId);
        }


        // Instructors
        public void AddInstructor(Guid instructorId)
        {
            Check.NotNull(instructorId, nameof(instructorId));

            if (IsInInstructor(instructorId))
            {
                return;
            }

            Instructors.Add(new CourseInstructor(courseId: Id, instructorId));
        }

        public void RemoveInstructor(Guid instructorId)
        {
            Check.NotNull(instructorId, nameof(instructorId));

            if (!IsInInstructor(instructorId))
            {
                return;
            }

            Instructors.RemoveAll(x => x.InstructorId == instructorId);
        }

        public void RemoveAllInstructorsExceptGivenIds(List<Guid> instructorIds)
        {
            Check.NotNullOrEmpty(instructorIds, nameof(instructorIds));

            Instructors.RemoveAll(x => !instructorIds.Contains(x.InstructorId));
        }

        public void RemoveAllInstructors()
        {
            Instructors.RemoveAll(x => x.CourseId == Id);
        }

        private bool IsInInstructor(Guid instructorId)
        {
            return Instructors.Any(x => x.InstructorId == instructorId);
        }
    }
}
