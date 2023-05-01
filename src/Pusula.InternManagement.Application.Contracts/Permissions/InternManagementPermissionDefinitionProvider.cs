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

        var educationsPermission = group.AddPermission(InternManagementPermissions.Educations.Default, L("Permission:Educations"));
        educationsPermission.AddChild(InternManagementPermissions.Educations.Create, L("Permission:Educations.Create"));
        educationsPermission.AddChild(InternManagementPermissions.Educations.Edit, L("Permission:Educations.Edit"));
        educationsPermission.AddChild(InternManagementPermissions.Educations.Delete, L("Permission:Educations.Delete"));

        var universitiesPermission = group.AddPermission(InternManagementPermissions.Universities.Default, L("Permission:Universities"));
        universitiesPermission.AddChild(InternManagementPermissions.Universities.Create, L("Permission:Universities.Create"));
        universitiesPermission.AddChild(InternManagementPermissions.Universities.Edit, L("Permission:Universities.Edit"));
        universitiesPermission.AddChild(InternManagementPermissions.Universities.Delete, L("Permission:Universities.Delete"));

        var universityDepartmentsPermission = group.AddPermission(InternManagementPermissions.UniversityDepartments.Default, L("Permission:UniversityDepartments"));
        universityDepartmentsPermission.AddChild(InternManagementPermissions.UniversityDepartments.Create, L("Permission:UniversityDepartments.Create"));
        universityDepartmentsPermission.AddChild(InternManagementPermissions.UniversityDepartments.Edit, L("Permission:UniversityDepartments.Edit"));
        universityDepartmentsPermission.AddChild(InternManagementPermissions.UniversityDepartments.Delete, L("Permission:UniversityDepartments.Delete"));

        var experiencePermission = group.AddPermission(InternManagementPermissions.Experiences.Default, L("Permission:Experiences"));
        experiencePermission.AddChild(InternManagementPermissions.Experiences.Create, L("Permission:Experiences.Create"));
        experiencePermission.AddChild(InternManagementPermissions.Experiences.Edit, L("Permission:Experiences.Edit"));
        experiencePermission.AddChild(InternManagementPermissions.Experiences.Delete, L("Permission:Experiences.Delete"));

        var projectsPermission = group.AddPermission(InternManagementPermissions.Projects.Default, L("Permission:Projects"));
        projectsPermission.AddChild(InternManagementPermissions.Projects.Create, L("Permission:Projects.Create"));
        projectsPermission.AddChild(InternManagementPermissions.Projects.Edit, L("Permission:Projects.Edit"));
        projectsPermission.AddChild(InternManagementPermissions.Projects.Delete, L("Permission:Projects.Delete"));

        var instructorsPermissions = group.AddPermission(InternManagementPermissions.Instructors.Default, L("Permission:Instructors"));
        instructorsPermissions.AddChild(InternManagementPermissions.Instructors.Create, L("Permission:Instructors.Create"));
        instructorsPermissions.AddChild(InternManagementPermissions.Instructors.Edit, L("Permission:Instructors.Edit"));
        instructorsPermissions.AddChild(InternManagementPermissions.Instructors.Delete, L("Permission:Instructors.Delete"));


        var adminGroup = context.AddGroup("", L("Permission:InternManagementAdminPermissions"));
        var adminPermissions = adminGroup.AddPermission("Admin Permissions", L("Permission:InternManagementAdminPermissions"));
        adminPermissions.AddChild(InternManagementPermissions.Interns.Admin, L("Permission:Interns.Admin"));
        adminPermissions.AddChild(InternManagementPermissions.Departments.Admin, L("Permission:Departments.Admin"));
        adminPermissions.AddChild(InternManagementPermissions.Educations.Admin, L("Permission:Educations.Admin"));
        adminPermissions.AddChild(InternManagementPermissions.Universities.Admin, L("Permission:Universities.Admin"));
        adminPermissions.AddChild(InternManagementPermissions.UniversityDepartments.Admin, L("Permission:UniversityDepartments.Admin"));
        adminPermissions.AddChild(InternManagementPermissions.Experiences.Admin, L("Permission:Experiences.Admin"));
        adminPermissions.AddChild(InternManagementPermissions.Projects.Admin, L("Permission:Projects.Admin"));
       
        adminPermissions.AddChild(InternManagementPermissions.Instructors.Admin, L("Permission:Instructors.Admin"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InternManagementResource>(name);
    }
}
