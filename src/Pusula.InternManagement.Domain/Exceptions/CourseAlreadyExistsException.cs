using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class CourseAlreadyExistsException : BusinessException
    {
        public CourseAlreadyExistsException(string name)
            : base(InternManagementDomainErrorCodes.CourseNameAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
