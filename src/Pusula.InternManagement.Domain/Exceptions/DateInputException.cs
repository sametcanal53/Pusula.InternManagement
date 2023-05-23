using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class DateInputException : BusinessException
    {
        public DateInputException()
            : base(InternManagementDomainErrorCodes.DateInputError)
        {
        }

        public DateInputException(string input)
            : base(InternManagementDomainErrorCodes.DateInputErrorWithParameters)
        {
            WithData("input", input);
        }
    }
}
