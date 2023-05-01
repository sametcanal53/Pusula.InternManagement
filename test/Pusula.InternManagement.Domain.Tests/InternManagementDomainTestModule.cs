using Pusula.InternManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Pusula.InternManagement;

[DependsOn(
    typeof(InternManagementEntityFrameworkCoreTestModule)
    )]
public class InternManagementDomainTestModule : AbpModule
{

}
