using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

#nullable disable
namespace Pusula.InternManagement.Works
{
    public class WorkDto : FullAuditedEntityDto<Guid>
    {
        //Intern
        public Guid InternId { get; set; }
        public string InternName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
