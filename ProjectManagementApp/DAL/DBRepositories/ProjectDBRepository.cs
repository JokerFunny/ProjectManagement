using Autofac;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DB implementation of <see cref="IProjectRepository"/>
    /// </summary>
    public class ProjectDBRepository : IProjectRepository
    {
        private readonly ProjectManagementContext _rProjectManagementContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ProjectDBRepository()
        {
            _rProjectManagementContext = IoC.Container.Resolve<ProjectManagementContext>();
        }

        /// <summary>
        /// <see cref="IProjectRepository.AddPorject(Project)"/>
        /// </summary>
        public bool AddPorject(Project project)
        {
            _rProjectManagementContext.Projects.Add(project);

            _rProjectManagementContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// <see cref="IProjectRepository.DeleteProject(Guid)"/>
        /// </summary>
        public bool DeleteProject(Guid id)
        {
            var project = _rProjectManagementContext.Projects.Where(p => p.Id == id).FirstOrDefault();

            if (project != null)
            {
                _rProjectManagementContext.Projects.Remove(project);
                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="IProjectRepository.GetAllProjects"/>
        /// </summary>
        public IEnumerable<Project> GetAllProjects()
            => _rProjectManagementContext.Projects;

        /// <summary>
        /// <see cref="IProjectRepository.GetProjectByCompany(Guid)"/>
        /// </summary>
        public IEnumerable<Project> GetProjectByCompany(Guid companyId)
            => _rProjectManagementContext.Projects
            .Where(p => p.DevelopedByCompany == companyId);

        /// <summary>
        /// <see cref="IProjectRepository.UpdateProject(Project)"/>
        /// </summary>
        public bool UpdateProject(Project project)
        {
            var projectFromDB = _rProjectManagementContext.Projects.Where(p => p.Id == project.Id).FirstOrDefault();

            if (projectFromDB != null)
            {
                if (!string.IsNullOrWhiteSpace(project.Name))
                    projectFromDB.Name = project.Name;

                if (!string.IsNullOrWhiteSpace(project.Description))
                    projectFromDB.Description = project.Description;

                if (project.DevelopedByCompany != Guid.Empty)
                    projectFromDB.DevelopedByCompany = project.DevelopedByCompany;

                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="IProjectRepository.GetProjectById(Guid)"/>
        /// </summary>
        public Project GetProjectById(Guid id)
            => _rProjectManagementContext.Projects
            .Where(p => p.Id == id)
            .FirstOrDefault();
    }
}