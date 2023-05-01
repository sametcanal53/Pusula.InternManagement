using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class ExperienceAlreadyExistsException : BusinessException
    {
        public ExperienceAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.ExperienceNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
