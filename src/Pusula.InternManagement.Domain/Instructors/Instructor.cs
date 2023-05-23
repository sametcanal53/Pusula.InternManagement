using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

#nullable disable
namespace Pusula.InternManagement.Instructors
{
    public class Instructor : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Title { get; private set; }

        public Instructor() { }
        public Instructor(Guid id, string name, string title) : base(id)
        {
            SetName(name);
            SetTitle(title);
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), InstructorConsts.MaxNameLength);
        }

        public void SetTitle(string title)
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), InstructorConsts.MaxTitleLength);
        }
    }
}
