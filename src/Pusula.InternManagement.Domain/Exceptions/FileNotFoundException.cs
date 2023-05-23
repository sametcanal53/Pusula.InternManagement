using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class FileNotFoundException : BusinessException
    {
        public FileNotFoundException(string name)
            : base(InternManagementDomainErrorCodes.FileNotFound)
        {
            WithData("name", name);
        }
    }
}
