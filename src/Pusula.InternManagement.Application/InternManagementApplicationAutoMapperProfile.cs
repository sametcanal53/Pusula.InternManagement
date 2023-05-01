using AutoMapper;
using Pusula.InternManagement.Departments;

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

    }
}
