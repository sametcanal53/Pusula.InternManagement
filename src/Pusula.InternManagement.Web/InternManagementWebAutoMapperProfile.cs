using AutoMapper;
using Pusula.InternManagement.Courses;
using Pusula.InternManagement.Departments;
using Pusula.InternManagement.Educations;
using Pusula.InternManagement.Experiences;
using Pusula.InternManagement.Files;
using Pusula.InternManagement.Instructors;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.Projects;
using Pusula.InternManagement.Universities;
using Pusula.InternManagement.UniversityDepartments;
using Pusula.InternManagement.Web.Pages.Instructors;
using Pusula.InternManagement.Web.Pages.Interns;
using Pusula.InternManagement.Works;
using Volo.Abp.AutoMapper;
using static Pusula.InternManagement.Web.Pages.Courses.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Courses.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Departments.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Departments.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Educations.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Educations.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Experiences.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Experiences.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Instructors.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Instructors.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Interns.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Interns.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Interns.InternModel;
using static Pusula.InternManagement.Web.Pages.Projects.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Projects.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Universities.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Universities.EditModalModel;
using static Pusula.InternManagement.Web.Pages.UniversityDepartments.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.UniversityDepartments.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Works.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Works.EditModalModel;

namespace Pusula.InternManagement.Web;

public class InternManagementWebAutoMapperProfile : Profile
{
    public InternManagementWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.

        CreateMap<CreateInternViewModel, CreateInternDto>();
        CreateMap<InternDto, EditInternViewModel>();
        CreateMap<EditInternViewModel, UpdateInternDto>();

        CreateMap<CreateDepartmentViewModel, CreateDepartmentDto>();
        CreateMap<DepartmentDto, EditDepartmentViewModel>();
        CreateMap<EditDepartmentViewModel, UpdateDepartmentDto>();

        CreateMap<CreateEducationViewModel, CreateEducationDto>();
        CreateMap<EducationDto, EditEducationViewModel>();
        CreateMap<EditEducationViewModel, UpdateEducationDto>();

        CreateMap<CreateUniversityViewModel, CreateUniversityDto>();
        CreateMap<UniversityDto, EditUniversityViewModel>();
        CreateMap<EditUniversityViewModel, UpdateUniversityDto>();

        CreateMap<CreateUniversityDepartmentViewModel, CreateUniversityDepartmentDto>();
        CreateMap<UniversityDepartmentDto, EditUniversityDepartmentViewModel>();
        CreateMap<EditUniversityDepartmentViewModel, UpdateUniversityDepartmentDto>();

        CreateMap<CreateExperienceViewModel, CreateExperienceDto>();
        CreateMap<ExperienceDto, EditExperienceViewModel>();
        CreateMap<EditExperienceViewModel, UpdateExperienceDto>();

        CreateMap<CreateProjectViewModel, CreateProjectDto>();
        CreateMap<ProjectDto, EditProjectViewModel>();
        CreateMap<EditProjectViewModel, UpdateProjectDto>();

        CreateMap<CreateInstructorViewModel, CreateInstructorDto>();
        CreateMap<InstructorDto, EditInstructorViewModel>();
        CreateMap<EditInstructorViewModel, UpdateInstructorDto>();

        CreateMap<CreateCourseViewModel, CreateCourseDto>();
        CreateMap<CourseDto, EditCourseViewModel>();
        CreateMap<EditCourseViewModel, UpdateCourseDto>();

        CreateMap<InternLookupDto, InternViewModel>().Ignore(x => x.IsSelected);
        CreateMap<InstructorLookupDto, InstructorViewModel>().Ignore(x => x.IsSelected);

        CreateMap<CreateWorkViewModel, CreateWorkDto>();
        CreateMap<WorkDto, EditWorkViewModel>();
        CreateMap<EditWorkViewModel, UpdateWorkDto>();

        CreateMap<InternWithDetailsDto, GetInternWithDetails>();


        CreateMap<FileDto, File>();
        CreateMap<CourseDto, Course>();
        CreateMap<EducationDto, Education>();
        CreateMap<ExperienceDto, Experience>();
        CreateMap<WorkDto, Work>();
        CreateMap<ProjectDto, Project>();
    }
}
