using AutoMapper;
using Pusula.InternManagement.Courses;
using Pusula.InternManagement.Departments;
using Pusula.InternManagement.Educations;
using Pusula.InternManagement.Experiences;
using Pusula.InternManagement.Instructors;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Projects;
using Pusula.InternManagement.Universities;
using Pusula.InternManagement.UniversityDepartments;
using Pusula.InternManagement.Works;

namespace Pusula.InternManagement;

public class InternManagementApplicationAutoMapperProfile : Profile
{
    public InternManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Department, DepartmentDto>();
        CreateMap<Department, DepartmentLookupDto>();

        CreateMap<Intern, InternDto>();
        CreateMap<Intern, InternLookupDto>();

        CreateMap<Education, EducationDto>();

        CreateMap<University, UniversityDto>();
        CreateMap<University, UniversityLookupDto>();
        
        CreateMap<UniversityDepartment, UniversityDepartmentDto>();
        CreateMap<UniversityDepartment, UniversityDepartmentLookupDto>();

        CreateMap<Experience, ExperienceDto>();
        
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectWithDetails, ProjectDto>();

        CreateMap<Instructor, InstructorDto>();
        CreateMap<Instructor, InstructorLookupDto>();

        CreateMap<Course, CourseDto>();
        CreateMap<CourseWithDetails, CourseDto>();

        CreateMap<Work, WorkDto>();

    }
}
