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

        var internsPermission = group.AddPermission(InternManagementPermissions.Interns.Default, L("Permission:Interns"));
        internsPermission.AddChild(InternManagementPermissions.Interns.Create, L("Permission:Interns.Create"));
        internsPermission.AddChild(InternManagementPermissions.Interns.Edit, L("Permission:Interns.Edit"));
        internsPermission.AddChild(InternManagementPermissions.Interns.Delete, L("Permission:Interns.Delete"));
        
        var departmentsPermission = group.AddPermission(InternManagementPermissions.Departments.Default, L("Permission:Departments"));
        departmentsPermission.AddChild(InternManagementPermissions.Departments.Create, L("Permission:Departments.Create"));
        departmentsPermission.AddChild(InternManagementPermissions.Departments.Edit, L("Permission:Departments.Edit"));
        departmentsPermission.AddChild(InternManagementPermissions.Departments.Delete, L("Permission:Departments.Delete"));

        var universityDepartmentsPermission = group.AddPermission(InternManagementPermissions.UniversityDepartments.Default, L("Permission:UniversityDepartments"));
        universityDepartmentsPermission.AddChild(InternManagementPermissions.UniversityDepartments.Create, L("Permission:UniversityDepartments.Create"));
        universityDepartmentsPermission.AddChild(InternManagementPermissions.UniversityDepartments.Edit, L("Permission:UniversityDepartments.Edit"));
        universityDepartmentsPermission.AddChild(InternManagementPermissions.UniversityDepartments.Delete, L("Permission:UniversityDepartments.Delete"));

        var adminGroup = context.AddGroup("", L("Permission:InternManagementAdminPermissions"));
        var adminPermissions = adminGroup.AddPermission("Admin Permissions", L("Permission:InternManagementAdminPermissions"));
        adminPermissions.AddChild(InternManagementPermissions.Interns.Admin, L("Permission:Interns.Admin"));
        adminPermissions.AddChild(InternManagementPermissions.Departments.Admin, L("Permission:Departments.Admin"));
        adminPermissions.AddChild(InternManagementPermissions.UniversityDepartments.Admin, L("Permission:UniversityDepartments.Admin"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InternManagementResource>(name);
    }
}
