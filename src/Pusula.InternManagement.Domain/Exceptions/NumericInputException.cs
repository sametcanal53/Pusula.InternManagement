using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.InternManagement.Exceptions
{
    public class NumericInputException : BusinessException
    {
        public NumericInputException(string input)
            : base(InternManagementDomainErrorCodes.NumericInputError)
        {
            WithData("input", input);
        }
    }
}
