using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Pusula.InternManagement.Web;

[Dependency(ReplaceServices = true)]
public class InternManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "InternManagement";
}
