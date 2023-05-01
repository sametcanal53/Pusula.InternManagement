using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.InternManagement.Courses
{
    public interface ICourseRepository : IRepository<Course, Guid>
    {
        // Retrieves a course with detailed information based on its ID.
        Task<CourseWithDetails> GetByIdAsync(Guid id);

        // Finds a course entity by its name
        Task<Course> FindByNameAsync(string name);

        // Gets a list of courses with their details based on the given input parameters
        Task<List<CourseWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        );
    }
}
