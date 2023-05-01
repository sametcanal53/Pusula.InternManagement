using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class FileNameAlreadyExistsException : BusinessException
    {
        public FileNameAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.FileNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
