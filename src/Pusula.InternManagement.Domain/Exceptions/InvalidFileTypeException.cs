using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class InvalidFileTypeException : BusinessException
    {
        public InvalidFileTypeException(string fileType)
            : base(InternManagementDomainErrorCodes.InvalidFileType)
        {
            WithData("fileType", fileType);
        }
    }
}
