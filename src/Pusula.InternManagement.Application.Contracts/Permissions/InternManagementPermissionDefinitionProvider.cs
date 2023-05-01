using Pusula.InternManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Pusula.InternManagement.Permissions;

public class InternManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(InternManagementPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(InternManagementPermissions.MyPermission1, L("Permission:MyPermission1"));

        var departmentsPermission = group.AddPermission(InternManagementPermissions.Departments.Default, L("Permission:Departments"));
        departmentsPermission.AddChild(InternManagementPermissions.Departments.Create, L("Permission:Departments.Create"));
        departmentsPermission.AddChild(InternManagementPermissions.Departments.Edit, L("Permission:Departments.Edit"));
        departmentsPermission.AddChild(InternManagementPermissions.Departments.Delete, L("Permission:Departments.Delete"));



        var adminGroup = context.AddGroup("", L("Permission:InternManagementAdminPermissions"));
        var adminPermissions = adminGroup.AddPermission("Admin Permissions", L("Permission:InternManagementAdminPermissions"));

        adminPermissions.AddChild(InternManagementPermissions.Departments.Admin, L("Permission:Departments.Admin"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InternManagementResource>(name);
    }
}
