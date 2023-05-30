using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.InternManagement.Files
{
    public class File : CreationAuditedAggregateRoot<Guid>
    {
        public Guid InternId { get; set; }
        public string Name { get; private set; }

        public File() { }
        public File(Guid id, Guid internId, string name) : base(id)
        {
            InternId = internId;
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), FileConsts.MaxNameLength);
        }
    }
}
