using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Projects
{
    public class InternProjectDto : EntityDto
    {
        public Guid ProjectId { get; set; }
        public Guid InternId { get; set; }

    }
}
