using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class UniversityAlreadyExistsException : BusinessException
    {
        public UniversityAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.UniversityNameAlreadyExists)
        {
            WithData("name", name);
        }

    }
}
