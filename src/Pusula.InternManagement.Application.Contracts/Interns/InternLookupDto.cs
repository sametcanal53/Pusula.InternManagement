using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

#nullable disable
namespace Pusula.InternManagement.Interns
{
    public class InternLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
