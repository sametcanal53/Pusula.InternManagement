using AutoMapper;
using Pusula.InternManagement.Departments;
using Pusula.InternManagement.Interns;
using Pusula.InternManagement.UniversityDepartments;
using Pusula.InternManagement.Web.Pages.Interns;
using Volo.Abp.AutoMapper;
using static Pusula.InternManagement.Web.Pages.Departments.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Departments.EditModalModel;
using static Pusula.InternManagement.Web.Pages.Interns.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Interns.EditModalModel;
using static Pusula.InternManagement.Web.Pages.UniversityDepartments.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.UniversityDepartments.EditModalModel;

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

        CreateMap<CreateUniversityDepartmentViewModel, CreateUniversityDepartmentDto>();
        CreateMap<UniversityDepartmentDto, EditUniversityDepartmentViewModel>();
        CreateMap<EditUniversityDepartmentViewModel, UpdateUniversityDepartmentDto>();


        CreateMap<InternLookupDto, InternViewModel>().Ignore(x => x.IsSelected);

    }
}
