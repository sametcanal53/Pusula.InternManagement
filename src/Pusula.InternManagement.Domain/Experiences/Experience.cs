using Pusula.InternManagement.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

namespace Pusula.InternManagement.Experiences
{
    public class Experience : FullAuditedAggregateRoot<Guid>
    {
        //Intern
        public Guid InternId { get; set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string CompanyName { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Experience() { }

        public Experience(Guid id, Guid internId, string name, string title, string description, string companyName, DateTime startDate, DateTime endDate) : base(id)
        {
            InternId = internId;
            SetName(name);
            SetTitle(title);
            SetDescription(description);
            SetCompanyName(companyName);
            SetStartDate(startDate);
            SetEndDate(endDate);
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), ExperienceConsts.MaxNameLength);
        }
        public void SetTitle(string title)
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), ExperienceConsts.MaxTitleLength);
        }
        public void SetDescription(string description)
        {
            Description = Check.NotNullOrWhiteSpace(description, nameof(description), ExperienceConsts.MaxDescriptionLength);
        }
        public void SetCompanyName(string companyName)
        {
            CompanyName = Check.NotNullOrWhiteSpace(companyName, nameof(companyName), ExperienceConsts.MaxCompanyNameLength);
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
