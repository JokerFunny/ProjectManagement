using Model;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for Project service
     /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Get list of <see cref="ProjectViewModel"/> by <paramref name="companyId"/>
        /// </summary>
        /// <param name="companyId">Target company</param>
        IEnumerable<ProjectViewModel> GetProjectViewModelByCompany(Guid companyId);

        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="project">Targer project</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if adding succesfull
        ///     False - if error happens
        /// </returns>
        bool AddPorject(Project project, out string errorMessage);

        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="project">Target new properties</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if update succesfull
        ///     False - if error happens
        /// </returns>
        bool UpdateProject(Project project, out string errorMessage);

        /// <summary>
        /// Delete user by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if delete succesfull
        ///     False - if error happens
        /// </returns>
        bool DeleteProject(Guid id, out string errorMessage);

        /// <summary>
        /// Gets all projects
        /// </summary>
        IEnumerable<Project> GetAllProjects();
    }
}
