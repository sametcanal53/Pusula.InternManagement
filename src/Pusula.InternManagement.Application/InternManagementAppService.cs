using System;
using System.Collections.Generic;
using System.Text;
using Pusula.InternManagement.Localization;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement;

/* Inherit your application services from this class.
 */
public abstract class InternManagementAppService : ApplicationService
{
    protected InternManagementAppService()
    {
        LocalizationResource = typeof(InternManagementResource);
    }
}
