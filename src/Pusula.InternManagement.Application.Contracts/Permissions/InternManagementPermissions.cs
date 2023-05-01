namespace Pusula.InternManagement.Permissions;

public static class InternManagementPermissions
{
    public const string GroupName = "InternManagement";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class Interns
    {
        public const string Default = GroupName + ".Interns";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }

    public static class Departments
    {
        public const string Default = GroupName + ".Departments";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }
    public static class Educations
    {
        public const string Default = GroupName + ".Educations";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }

    public static class Universities
    {
        public const string Default = GroupName + ".Universities";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }

    public static class UniversityDepartments
    {
        public const string Default = GroupName + ".UniversityDepartments";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }

    public static class Experiences
    {
        public const string Default = GroupName + ".Experiences";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }

    public static class Projects
    {
        public const string Default = GroupName + ".Projects";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }

    public static class Courses
    {
        public const string Default = GroupName + ".Courses";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }

    public static class Instructors
    {
        public const string Default = GroupName + ".Instructors";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Admin = Default + ".Admin";
    }



}
