namespace Pusula.InternManagement;

public static class InternManagementDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */

    public const string InternNameAlreadyExists = "InternManagementException:0";
    public const string DepartmentNameAlreadyExists = "InternManagementException:1";
    public const string UniversityNameAlreadyExists = "InternManagementException:2";
    public const string EducationNameAlreadyExists = "InternManagementException:3";
    public const string UniversityDepartmentNameAlreadyExists = "InternManagementException:4";
    public const string ExperienceNameAlreadyExists = "InternManagementException:5";
    public const string ProjectNameAlreadyExists = "InternManagementException:6";
    public const string CourseNameAlreadyExists = "InternManagementException:7";
    public const string InstructorNameAlreadyExists = "InternManagementException:8";

    public const string DateInputError = "InternManagementException:12";

}
