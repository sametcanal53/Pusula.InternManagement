using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class InternAlreadyExistsException : BusinessException
    {
        public InternAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.InternNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
