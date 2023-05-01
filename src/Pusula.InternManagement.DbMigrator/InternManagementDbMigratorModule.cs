using Pusula.InternManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Pusula.InternManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InternManagementEntityFrameworkCoreModule),
    typeof(InternManagementApplicationContractsModule)
    )]
public class InternManagementDbMigratorModule : AbpModule
{

}
