using Pusula.InternManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Pusula.InternManagement.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class InternManagementPageModel : AbpPageModel
{
    protected InternManagementPageModel()
    {
        LocalizationResourceType = typeof(InternManagementResource);
    }
}
