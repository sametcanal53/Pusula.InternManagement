using System.Threading.Tasks;

namespace Pusula.InternManagement.Data;

public interface IInternManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
