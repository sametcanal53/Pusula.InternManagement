using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Pusula.InternManagement.Experiences
{
    public class ExperienceDto : FullAuditedEntityDto<Guid>
    {
        //Intern
        public Guid InternId { get; set; }
        public string InternName { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
