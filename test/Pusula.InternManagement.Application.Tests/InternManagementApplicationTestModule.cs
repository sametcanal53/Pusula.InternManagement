using Volo.Abp.Modularity;

namespace Pusula.InternManagement;

[DependsOn(
    typeof(InternManagementApplicationModule),
    typeof(InternManagementDomainTestModule)
    )]
public class InternManagementApplicationTestModule : AbpModule
{

}
