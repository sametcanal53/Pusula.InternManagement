using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.InternManagement.Projects
{
    public class ProjectIntern : Entity
    {
        public Guid ProjectId { get; set; }
        public Guid InternId { get; set; }

        public ProjectIntern(Guid projectId, Guid internId)
        {
            ProjectId = projectId;
            InternId = internId;
        }

        public override object[] GetKeys()
        {
            return new object[] { ProjectId, InternId };
        }
    }
}
