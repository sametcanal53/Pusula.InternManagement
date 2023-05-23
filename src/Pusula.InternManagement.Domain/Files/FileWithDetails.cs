using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;


#nullable disable
namespace Pusula.InternManagement.Files
{
    public class FileWithDetails : IHasCreationTime
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid InternId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
