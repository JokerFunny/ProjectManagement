using DAL.Interfaces;
using Model;
using RAMStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// RAM implementation of <see cref="IProjectRepository"/>
    /// </summary>
    public class ProjectRAMRepository : IProjectRepository
    {
        /// <summary>
        /// <see cref="IProjectRepository.AddPorject(Project)"/>
        /// </summary>
        public bool AddPorject(Project project)
        {
            Storage.AddProject(project);

            return true;
        }

        /// <summary>
        /// <see cref="IProjectRepository.DeleteProject(Guid)"/>
        /// </summary>
        public bool DeleteProject(Guid id)
        {
            Storage.DeleteProject(id);

            return true;
        }

        /// <summary>
        /// <see cref="IProjectRepository.GetAllProjects"/>
        /// </summary>
        public IEnumerable<Project> GetAllProjects()
            => Storage.Projects;

        /// <summary>
        /// <see cref="IProjectRepository.GetProjectByCompany(Guid)"/>
        /// </summary>
        public IEnumerable<Project> GetProjectByCompany(Guid companyId)
            => Storage.Projects.Where(p => p.DevelopedByCompany == companyId);

        /// <summary>
        /// <see cref="IProjectRepository.UpdateProject(Project)"/>
        /// </summary>
        public bool UpdateProject(Project project)
        {
            Storage.UpdateProject(project);

            return true;
        }

        /// <summary>
        /// <see cref="IProjectRepository.GetProjectById(Guid)"/>
        /// </summary>
        public Project GetProjectById(Guid id)
            => Storage.Projects.Where(p => p.Id == id)
            .FirstOrDefault();
    }
}
