using Pusula.InternManagement.Instructors;
using Pusula.InternManagement.Interns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.InternManagement.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        // GetListAsync method returns a paged list of courses based on the given input parameters
        Task<PagedResultDto<CourseDto>> GetListAsync(CourseGetListInput input);

        // GetAsync method returns a course entity with the given ID
        Task<CourseDto> GetAsync(Guid id);

        // CreateAsync method creates a new course entity with the given input data
        Task CreateAsync(CreateCourseDto input);

        // UpdateAsync method updates an existing course entity with the given input data
        Task UpdateAsync(Guid id, UpdateCourseDto input);

        // DeleteAsync method deletes an existing course entity with the given ID
        Task DeleteAsync(Guid id);

        // GetInstructorLookupAsync method returns a list of instructors for lookup purposes
        Task<ListResultDto<InstructorLookupDto>> GetInstructorLookupAsync();

        // GetInternLookupAsync method returns a list of interns for lookup purposes
        Task<ListResultDto<InternLookupDto>> GetInternLookupAsync();
    }
}
