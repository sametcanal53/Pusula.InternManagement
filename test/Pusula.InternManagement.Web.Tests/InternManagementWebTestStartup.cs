using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Pusula.InternManagement;

public class InternManagementWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<InternManagementWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
