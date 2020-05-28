using Model;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for Project repository
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Get list of <see cref="Project"/> by <paramref name="companyId"/>
        /// </summary>
        /// <param name="companyId">Target company</param>
        IEnumerable<Project> GetProjectByCompany(Guid companyId);

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="project">Targer project</param>
        bool AddPorject(Project project);

        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="project">Target new properties</param>
        bool UpdateProject(Project project);

        /// <summary>
        /// Delete user by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        bool DeleteProject(Guid id);

        /// <summary>
        /// Gets all projects
        /// </summary>
        IEnumerable<Project> GetAllProjects();

        /// <summary>
        /// Get <see cref="Project"/> by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        Project GetProjectById(Guid id);
    }
}
