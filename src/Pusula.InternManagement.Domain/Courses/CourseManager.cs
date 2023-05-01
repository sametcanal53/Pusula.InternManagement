using JetBrains.Annotations;
using Pusula.InternManagement.Instructors;
using Pusula.InternManagement.Interns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Pusula.InternManagement.Courses
{
    public class CourseManager : DomainService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IInternRepository _internRepository;

        public CourseManager(
            ICourseRepository courseRepository,
            IInstructorRepository instructorRepository,
            IInternRepository internRepository)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _internRepository = internRepository;
        }

        public async Task<Course> CreateAsync(
            string name,
        string description,
        DateTime publishDate,
            [CanBeNull] List<string> interns,
            [CanBeNull] List<string> instructors)
        {
            var course = new Course(GuidGenerator.Create(), name, description, publishDate);

            await SetInternsAsync(course, interns);
            await SetInstructorAsync(course, instructors);

            await _courseRepository.InsertAsync(course);
            return course;
        }

        public async Task UpdateAsync(
            Course course,
            string name,
            string description,
            DateTime publishDate,
            [CanBeNull] List<string> interns,
            [CanBeNull] List<string> instructors)
        {
            await _courseRepository.EnsureCollectionLoadedAsync(course, c => c.Interns);
            await _courseRepository.EnsureCollectionLoadedAsync(course, c => c.Instructors);

            course.SetName(name);
            course.SetDescription(description);
            course.SetPublishDate(publishDate);

            await SetInternsAsync(course, interns);
            await SetInstructorAsync(course, instructors);

            await _courseRepository.UpdateAsync(course);
        }

        private async Task SetInternsAsync(Course course, List<string> internNames)
        {
            var existingInternIds = course.Interns.Select(i => i.InternId).ToList();
            if (internNames == null || internNames.Count == 0)
            {
                course.RemoveAllInterns();
                return;
            }

            var internIds = new List<Guid>();
            if (internNames != null && internNames.Any())
            {
                var interns = await _internRepository.GetListAsync(i => internNames.Contains(i.Name));
                internIds = interns.Select(i => i.Id).ToList();
            }

            var newInternIds = internIds.Except(existingInternIds).ToList();
            foreach (var internId in newInternIds)
            {
                course.AddIntern(internId);
            }

            var internsToRemove = course.Interns.Where(i => !internIds.Contains(i.InternId)).ToList();
            foreach (var intern in internsToRemove)
            {
                course.RemoveIntern(intern.InternId);
            }
        }

        private async Task SetInstructorAsync(Course course, List<string> instructorNames)
        {
            var existingInstructorIds = course.Instructors.Select(i => i.InstructorId).ToList();
            if (instructorNames == null || instructorNames.Count == 0)
            {
                course.RemoveAllInstructors();
                return;
            }

            var instructorIds = new List<Guid>();
            if (instructorNames != null && instructorNames.Any())
            {
                var instructors = await _instructorRepository.GetListAsync(i => instructorNames.Contains(i.Name));
                instructorIds = instructors.Select(i => i.Id).ToList();
            }

            var newInstructorIds = instructorIds.Except(existingInstructorIds).ToList();
            foreach (var instructorId in newInstructorIds)
            {
                course.AddInstructor(instructorId);
            }

            var instructorsToRemove = course.Instructors.Where(i => !instructorIds.Contains(i.InstructorId)).ToList();
            foreach (var instructor in instructorsToRemove)
            {
                course.RemoveInstructor(instructor.InstructorId);
            }
        }

    }
}
