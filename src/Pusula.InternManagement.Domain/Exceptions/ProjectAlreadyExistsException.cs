using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class ProjectAlreadyExistsException : BusinessException
    {
        public ProjectAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.ProjectNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
