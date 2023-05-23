using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

#nullable disable
namespace Pusula.InternManagement.Universities
{
    public class University : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }

        public University() { }
        public University(Guid id, string name) : base(id)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), UniversityConsts.MaxNameLength);
        }
    }
}
