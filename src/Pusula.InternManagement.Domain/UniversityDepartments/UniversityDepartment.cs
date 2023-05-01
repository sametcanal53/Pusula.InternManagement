using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;

#nullable disable
namespace Pusula.InternManagement.UniversityDepartments
{
    public class UniversityDepartment : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }

        public UniversityDepartment() { }
        public UniversityDepartment(Guid id, string name) : base(id)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), UniversityDepartmentConsts.MaxNameLength);
        }
    }
}
