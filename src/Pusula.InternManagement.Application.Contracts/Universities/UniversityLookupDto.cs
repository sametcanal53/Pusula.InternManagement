using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Universities
{
    public class UniversityLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }

    }
}
