using Pusula.InternManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Pusula.InternManagement.Permissions;

public class InternManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InternManagementPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(InternManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InternManagementResource>(name);
    }
}
