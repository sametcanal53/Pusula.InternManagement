using JetBrains.Annotations;
using Pusula.InternManagement.Interns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Pusula.InternManagement.Projects
{
    public class ProjectManager : DomainService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IInternRepository _internRepository;

        public ProjectManager(
            IProjectRepository projectRepository,
            IInternRepository internRepository)
        {
            _projectRepository = projectRepository;
            _internRepository = internRepository;
        }

        public async Task<Project> CreateAsync(string name, string description, DateTime startDate, DateTime endDate, [CanBeNull] List<string> interns)
        {
            var project = new Project(GuidGenerator.Create(), name, description, startDate, endDate);

            await SetInternsAsync(project, interns);

            await _projectRepository.InsertAsync(project);
            return project;
        }

        public async Task UpdateAsync(
            Project project,
            string name,
            string description,
            DateTime startDate,
            DateTime endDate,
            [CanBeNull] List<string> interns
        )
        {
            await _projectRepository.EnsureCollectionLoadedAsync(project, p => p.Interns);

            project.SetName(name);
            project.SetDescription(description);
            project.SetStartDate(startDate);
            project.SetEndDate(endDate);

            await SetInternsAsync(project, interns);
            await _projectRepository.UpdateAsync(project);
        }

        private async Task SetInternsAsync(Project project, List<string> internNames)
        {
            var existingInternIds = project.Interns.Select(i => i.InternId).ToList();
            if (internNames == null || internNames.Count == 0)
            {
                project.RemoveAllInterns();
                return;
            }

            var internIds = new List<Guid>();
            if (internNames != null && internNames.Any())
            {
                var interns = await _internRepository.GetListAsync(i => internNames.Contains(i.Name));
                internIds = interns.Select(i => i.Id).ToList();
            }

            // add new interns
            var newInternIds = internIds.Except(existingInternIds).ToList();
            foreach (var internId in newInternIds)
            {
                project.AddIntern(internId);
            }

            // remove interns not in the given list
            var internsToRemove = project.Interns.Where(i => !internIds.Contains(i.InternId)).ToList();
            foreach (var intern in internsToRemove)
            {
                project.RemoveIntern(intern.InternId);
            }
        }

    }
}
