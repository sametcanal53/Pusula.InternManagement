namespace Pusula.InternManagement.Permissions;

public static class InternManagementPermissions
{
    public const string GroupName = "InternManagement";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class Departments
    {
        public const string Default = GroupName + ".Departments";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }
}
