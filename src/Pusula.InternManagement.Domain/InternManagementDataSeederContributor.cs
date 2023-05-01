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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Pusula.InternManagement
{
    public class InternManagementDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IInternRepository _internRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IUniversityDepartmentRepository _universityDepartmentRepository;
        private readonly IExperienceRepository _experienceRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRepository<ProjectIntern> _projectInternRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IRepository<CourseIntern> _courseInternRepository;
        private readonly IRepository<CourseInstructor> _courseInstructorRepository;
        private readonly IWorkRepository _workRepository;

        public InternManagementDataSeederContributor(
            IGuidGenerator guidGenerator,
            IInternRepository internRepository,
            IDepartmentRepository departmentRepository,
            IEducationRepository educationRepository,
            IUniversityRepository universityRepository,
            IUniversityDepartmentRepository universityDepartmentRepository,
            IExperienceRepository experienceRepository,
            IProjectRepository projectRepository,
            IRepository<ProjectIntern> projectInternRepository,
            ICourseRepository courseRepository,
            IInstructorRepository instructorRepository,
            IRepository<CourseIntern> courseInternRepository,
            IRepository<CourseInstructor> courseInstructorRepository,
            IWorkRepository workRepository)
        {
            _guidGenerator = guidGenerator;
            _internRepository = internRepository;
            _departmentRepository = departmentRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _universityDepartmentRepository = universityDepartmentRepository;
            _experienceRepository = experienceRepository;
            _projectRepository = projectRepository;
            _projectInternRepository = projectInternRepository;
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _courseInternRepository = courseInternRepository;
            _courseInstructorRepository = courseInstructorRepository;
            _workRepository = workRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {

            // New Department
            var softwareDevelopment = await _departmentRepository.InsertAsync(new Department(_guidGenerator.Create(), "Software Development"), autoSave: true);
            var humanResource = await _departmentRepository.InsertAsync(new Department(_guidGenerator.Create(), "Human Resource"), autoSave: true);
            var it = await _departmentRepository.InsertAsync(new Department(_guidGenerator.Create(), "IT"), autoSave: true);

            // New Intern
            var intern1 = await _internRepository.InsertAsync(
                   new Intern(
                       _guidGenerator.Create(),
                       softwareDevelopment.Id,
                       "Sametcan AL",
                       "5393883197",
                       "sametcanal53@gmail.com",
                       new DateTime(2023, 3, 6),
                       new DateTime(2023, 6, 9)), autoSave: true);

            var intern2 = await _internRepository.InsertAsync(
                    new Intern(
                       _guidGenerator.Create(),
                       humanResource.Id,
                       "Yıldız Aksu",
                       "5352215111",
                       "test@gmail.com",
                       new DateTime(2010, 11, 5),
                       new DateTime(2023, 3, 30)), autoSave: true);

            var intern3 = await _internRepository.InsertAsync(
                    new Intern(
                       _guidGenerator.Create(),
                       softwareDevelopment.Id,
                       "Ahmet Yılmaz",
                       "5352451124",
                       "ahmet.yilmaz@gmail.com",
                       new DateTime(2015, 9, 19),
                       new DateTime(2020, 12, 30)), autoSave: true);

            // New Experience
            await _experienceRepository.InsertAsync(

                new Experience(
                    _guidGenerator.Create(),
                    intern1.Id,
                    "Experience-1",
                    "Back End Developer",
                    "A backend developer is responsible for writing APIs, designing libraries, improving data architecture, and communicating with the database. The backend developer has good knowledge of backend programming languages, servers, databases, and APIs.",
                    "Company 1",
                    new DateTime(2019, 5, 3),
                    new DateTime(2023, 1, 9)), autoSave: true);


            await _experienceRepository.InsertAsync(
                new Experience(
                    _guidGenerator.Create(),
                    intern2.Id,
                    "Experience-2",
                    "Front End Developer",
                    "The main responsibility of a Front-End Developer is the User interface.",
                    "Company 2",
                    new DateTime(2020, 1, 5),
                    new DateTime(2025, 5, 1)), autoSave: true);

            // New University Department
            var ce = await _universityDepartmentRepository.InsertAsync(new UniversityDepartment(_guidGenerator.Create(), "Computer Engineering"), autoSave: true);
            var ba = await _universityDepartmentRepository.InsertAsync(new UniversityDepartment(_guidGenerator.Create(), "Business Administration"), autoSave: true);

            // New University

            var nu = await _universityRepository.InsertAsync(new University(_guidGenerator.Create(), "Nisantasi University"), autoSave: true);
            var iu = await _universityRepository.InsertAsync(new University(_guidGenerator.Create(), "Istanbul University"), autoSave: true);
            var itu = await _universityRepository.InsertAsync(new University(_guidGenerator.Create(), "İstanbul Teknik University"), autoSave: true);

            // New Education
            await _educationRepository.InsertAsync(new Education(_guidGenerator.Create(), nu.Id, ce.Id, intern1.Id, "Education-1", Grade.University_1, 95.04f, new DateTime(2019, 7, 5), new DateTime(2023, 6, 9)), autoSave: true);
            await _educationRepository.InsertAsync(new Education(_guidGenerator.Create(), nu.Id, ba.Id, intern2.Id, "Education-2", Grade.University_2, 85.74f, new DateTime(2015, 1, 7), new DateTime(2018, 3, 11)), autoSave: true);
            await _educationRepository.InsertAsync(new Education(_guidGenerator.Create(), iu.Id, ba.Id, intern1.Id, "Education-3", Grade.MasterDegree, 72.74f, new DateTime(2019, 1, 1), new DateTime(2025, 1, 27)), autoSave: true);

            // New Project
            var project1 = await _projectRepository.InsertAsync(
                new Project(
                    _guidGenerator.Create(),
                    "Task Mate",
                    "Kullanıcı dostu görev yönetim yazılımı: TaskMate.",
                    new DateTime(2015, 4, 5),
                    new DateTime(2020, 5, 6)));

            var project2 = await _projectRepository.InsertAsync(
                new Project(
                    _guidGenerator.Create(),
                    "ChatBotX",
                    "ChatBotX, işletmelerin müşteri hizmetlerini iyileştirmelerine yardımcı olan bir yapay zeka destekli sohbet botudur.",
                    new DateTime(2020, 9, 11),
                    new DateTime(2024, 1, 30)));

            // New Intern Project - Relation

            await _projectInternRepository.InsertAsync(
                new ProjectIntern(project1.Id, intern1.Id));

            await _projectInternRepository.InsertAsync(
                new ProjectIntern(project1.Id, intern2.Id));

            await _projectInternRepository.InsertAsync(
                new ProjectIntern(project2.Id, intern3.Id));

            await _projectInternRepository.InsertAsync(
                new ProjectIntern(project1.Id, intern3.Id));

            await _projectInternRepository.InsertAsync(
                new ProjectIntern(project2.Id, intern1.Id));

            // New Instructor
            var instructor1 = await _instructorRepository.InsertAsync(new Instructor(_guidGenerator.Create(), "Instructor-1", "Product Manager"), autoSave: true);
            var instructor2 = await _instructorRepository.InsertAsync(new Instructor(_guidGenerator.Create(), "Instructor-2", "Founder"), autoSave: true);
            var instructor3 = await _instructorRepository.InsertAsync(new Instructor(_guidGenerator.Create(), "Instructor-3", "Co-Founder"), autoSave: true);

            // New Course
            var course1 = await _courseRepository.InsertAsync(new Course(_guidGenerator.Create(), "İngilizce-1", "Temel İngilizce Eğitimi", new DateTime(2021, 5, 8)), autoSave: true);
            var course2 = await _courseRepository.InsertAsync(new Course(_guidGenerator.Create(), "İşletme-1", "Temel İşletme Eğitimi", new DateTime(2021, 11, 30)), autoSave: true);
            var course3 = await _courseRepository.InsertAsync(new Course(_guidGenerator.Create(), "İşletme-2", "Orta Seviye İşletme Eğitimi", new DateTime(2021, 5, 11)), autoSave: true);

            // New Course Intern- Relation
            await _courseInternRepository.InsertAsync(new CourseIntern(course1.Id, intern1.Id), autoSave: true);
            await _courseInternRepository.InsertAsync(new CourseIntern(course2.Id, intern1.Id), autoSave: true);
            await _courseInternRepository.InsertAsync(new CourseIntern(course3.Id, intern2.Id), autoSave: true);
            await _courseInternRepository.InsertAsync(new CourseIntern(course1.Id, intern2.Id), autoSave: true);
            await _courseInternRepository.InsertAsync(new CourseIntern(course1.Id, intern3.Id), autoSave: true);

            // New Course Instructor - Relation
            await _courseInstructorRepository.InsertAsync(new CourseInstructor(course1.Id, instructor1.Id), autoSave: true);
            await _courseInstructorRepository.InsertAsync(new CourseInstructor(course1.Id, instructor2.Id), autoSave: true);
            await _courseInstructorRepository.InsertAsync(new CourseInstructor(course2.Id, instructor1.Id), autoSave: true);
            await _courseInstructorRepository.InsertAsync(new CourseInstructor(course3.Id, instructor1.Id), autoSave: true);

            // New File (Controlled by Azure Blob Storage)
            //var file1 = await _fileRepository.InsertAsync(new File(_guidGenerator.Create(), intern1.Id,"File-1"));
            //var file2 = await _fileRepository.InsertAsync(new File(_guidGenerator.Create(), intern1.Id, "File-2"));
            //var file3 = await _fileRepository.InsertAsync(new File(_guidGenerator.Create(), intern3.Id, "File-3"));

            // New work
            await _workRepository.InsertAsync(new Work(_guidGenerator.Create(), intern1.Id, "Work-1", "Work-1 Description", new DateTime(2022, 1, 5)));
            await _workRepository.InsertAsync(new Work(_guidGenerator.Create(), intern2.Id, "Work-2", "Work-2 Description", new DateTime(2011, 9, 15)));
            await _workRepository.InsertAsync(new Work(_guidGenerator.Create(), intern3.Id, "Work-3", "Work-3 Description", new DateTime(2019, 5, 22)));
            await _workRepository.InsertAsync(new Work(_guidGenerator.Create(), intern1.Id, "Work-4", "Work-4 Description", new DateTime(2018, 5, 30)));
        }
    }
}
