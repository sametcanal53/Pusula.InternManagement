using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

namespace Pusula.InternManagement.Works
{
    public class Work : FullAuditedAggregateRoot<Guid>
    {
        // Intern
        public Guid InternId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }

        public Work(Guid id, Guid internId, string name, string description, DateTime date) : base(id)
        {
            InternId = internId;
            SetName(name);
            SetDescription(description);
            SetDate(date);
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), WorkConsts.MaxNameLength);
        }

        public void SetDescription(string description)
        {
            Description = Check.NotNullOrWhiteSpace(description, nameof(description), WorkConsts.MaxDescriptionLength);
        }

        public void SetDate(DateTime date)
        {
            Date = date;
        }
    }
}
