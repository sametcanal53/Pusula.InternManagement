using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class DepartmentAlreadyExistsException : BusinessException
    {
        public DepartmentAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.DepartmentNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
