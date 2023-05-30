using Pusula.InternManagement.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

namespace Pusula.InternManagement.Educations
{
    public class Education : FullAuditedAggregateRoot<Guid>
    {
        // University
        public Guid UniversityId { get; set; }
        // University Department
        public Guid UniversityDepartmentId { get; set; }
        //Intern
        public Guid InternId { get; set; }
        public string Name { get; private set; }
        public Grade Grade { get; private set; }
        public float GradePointAverage { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }


        public Education() { }

        public Education(Guid id, Guid universityId, Guid universityDepartmentId, Guid internId, string name, Grade grade, float gradePointAverage, DateTime startDate, DateTime endDate) : base(id)
        {
            UniversityId = universityId;
            UniversityDepartmentId = universityDepartmentId;
            InternId = internId;
            SetName(name);
            SetGrade(grade);
            SetGradePointAverage(gradePointAverage);
            SetStartDate(startDate);
            SetEndDate(endDate);
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), EducationConsts.MaxNameLength);
        }

        public void SetGrade(Grade grade)
        {
            Grade = grade;
        }

        public void SetGradePointAverage(float gradePointAverage)
        {
            GradePointAverage = gradePointAverage;
        }

        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

        public void SetEndDate(DateTime endDate)
        {
            if (endDate < StartDate)
                throw new DateInputException();
            EndDate = endDate;
        }

    }
}
