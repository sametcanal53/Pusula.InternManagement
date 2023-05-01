using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class InstructorAlreadyExistsException : BusinessException
    {
        public InstructorAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.InstructorNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
