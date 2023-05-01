using Pusula.InternManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Pusula.InternManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class InternManagementController : AbpControllerBase
{
    protected InternManagementController()
    {
        LocalizationResource = typeof(InternManagementResource);
    }
}
