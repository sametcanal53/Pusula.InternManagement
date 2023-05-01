using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class EducationAlreadyExistsException : BusinessException
    {
        public EducationAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.EducationNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
