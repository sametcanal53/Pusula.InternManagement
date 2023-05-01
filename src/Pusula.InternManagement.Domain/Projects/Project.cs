using Pusula.InternManagement.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

#nullable disable
namespace Pusula.InternManagement.Projects
{
    public class Project : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public ICollection<InternProject> Interns { get; private set; }
        public Project() { }
        public Project(Guid id, string name, string description, DateTime startDate, DateTime endDate) : base(id)
        {
            SetName(name);
            SetDescription(description);
            SetStartDate(startDate);
            SetEndDate(endDate);

            Interns = new Collection<InternProject>();
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProjectConsts.MaxNameLength);
        }

        public void SetDescription(string description)
        {
            Description = Check.NotNullOrWhiteSpace(description, nameof(description), ProjectConsts.MaxDescriptionLength);
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


        public void AddIntern(Guid internId)
        {
            Check.NotNull(internId, nameof(internId));

            if (IsInIntern(internId))
            {
                return;
            }

            Interns.Add(new InternProject(projectId: Id, internId: internId));
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
            Interns.RemoveAll(x => x.ProjectId == Id);
        }

        private bool IsInIntern(Guid internId)
        {
            return Interns.Any(x => x.InternId == internId);
        }

    }
}
