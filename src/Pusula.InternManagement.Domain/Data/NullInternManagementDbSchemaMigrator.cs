using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Pusula.InternManagement.Data;

/* This is used if database provider does't define
 * IInternManagementDbSchemaMigrator implementation.
 */
public class NullInternManagementDbSchemaMigrator : IInternManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
