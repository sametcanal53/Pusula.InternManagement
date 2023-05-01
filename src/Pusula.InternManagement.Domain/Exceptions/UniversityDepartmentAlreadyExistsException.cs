using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class UniversityDepartmentAlreadyExistsException : BusinessException
    {
        public UniversityDepartmentAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.UniversityDepartmentNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
