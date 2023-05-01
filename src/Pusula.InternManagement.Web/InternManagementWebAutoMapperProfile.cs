using AutoMapper;
using Pusula.InternManagement.Departments;
using static Pusula.InternManagement.Web.Pages.Departments.CreateModalModel;
using static Pusula.InternManagement.Web.Pages.Departments.EditModalModel;

namespace Pusula.InternManagement.Web;

public class InternManagementWebAutoMapperProfile : Profile
{
    public InternManagementWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.

        CreateMap<CreateDepartmentViewModel, CreateDepartmentDto>();
        CreateMap<DepartmentDto, EditDepartmentViewModel>();
        CreateMap<EditDepartmentViewModel, UpdateDepartmentDto>();

    }
}
