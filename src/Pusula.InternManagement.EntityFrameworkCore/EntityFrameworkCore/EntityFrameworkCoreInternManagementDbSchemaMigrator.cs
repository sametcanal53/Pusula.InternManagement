using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pusula.InternManagement.Data;
using Volo.Abp.DependencyInjection;

namespace Pusula.InternManagement.EntityFrameworkCore;

public class EntityFrameworkCoreInternManagementDbSchemaMigrator
    : IInternManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreInternManagementDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the InternManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<InternManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}
