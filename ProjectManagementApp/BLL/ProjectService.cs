using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// <see cref="IProjectService"/>
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _rProjectRepository;
        private readonly IFormulaRepository _rFormulaRepository;
        private readonly ICompanyRepository _rCompanyRepository;
        private readonly IMaterialRepository _rMaterialRepository;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="projectRepository"><see cref="IProjectRepository"/></param>
        /// <param name="formulaRepository"><see cref="IFormulaRepository"/></param>
        /// <param name="companyRepository"><see cref="ICompanyRepository"/></param>
        /// <param name="materialRepository"><see cref="IMaterialRepository"/></param>
        public ProjectService(IProjectRepository projectRepository,
            IFormulaRepository formulaRepository,
            ICompanyRepository companyRepository, 
            IMaterialRepository materialRepository)
        {
            _rProjectRepository = projectRepository;
            _rFormulaRepository = formulaRepository;
            _rCompanyRepository = companyRepository;
            _rMaterialRepository = materialRepository;
        }

        /// <summary>
        /// <see cref="IProjectRepository.AddPorject(Project)"/>
        /// </summary>
        public bool AddPorject(Project project, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (project == null)
            {
                errorMessage = "Project can`t be null";
                return false;
            }

            Project projectFromRepository = _rProjectRepository.GetAllProjects()
                .Where(p => p.Name == project.Name 
                && p.DevelopedByCompany == project.DevelopedByCompany)
                .FirstOrDefault();

            if (projectFromRepository != null)
            {
                errorMessage = "The same project already exist";
                return false;
            }

            return _rProjectRepository.AddPorject(project);
        }

        /// <summary>
        /// <see cref="IProjectRepository.DeleteProject(Guid)"/>
        /// </summary>
        public bool DeleteProject(Guid id, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (id == Guid.Empty)
            {
                errorMessage = "Id can`t be empty";
                return false;
            }

            Project projectFromRepository = _rProjectRepository.GetProjectById(id);

            if (projectFromRepository == null)
            {
                errorMessage = "Project don`t exist";
                return false;
            }

            return _rProjectRepository.DeleteProject(id);
        }

        /// <summary>
        /// <see cref="IProjectRepository.GetAllProjects"/>
        /// </summary>
        public IEnumerable<Project> GetAllProjects()
            => _rProjectRepository.GetAllProjects();

        /// <summary>
        /// <see cref="IProjectRepository.AddPorject(Project)"/>
        /// </summary>
        public IEnumerable<ProjectViewModel> GetProjectViewModelByCompany(Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new ArgumentNullException("CompanyId can`t be empty");

            var projects = _rProjectRepository.GetProjectByCompany(companyId);
            var projectsView = new List<ProjectViewModel>();

            foreach (var project in projects)
            {
                Formula targetFormula = _rFormulaRepository.GetFormulaById(project.Formula);

                decimal totalPrice = 0M;

                if (targetFormula.MaterialsWithPercentQuantity != null)
                {
                    foreach (var material in targetFormula.MaterialsWithPercentQuantity)
                    {
                        totalPrice += _rMaterialRepository.GetMaterialById(material.Key).PricePerGramm * material.Value
                            * targetFormula.WeightInGramms / 100;
                    }
                }

                projectsView.Add(new ProjectViewModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    FormulaName = targetFormula.Name,
                    Weight = targetFormula.WeightInGramms,
                    TotalPrice = totalPrice,
                    DevelopedByCompany = _rCompanyRepository.GetCompanyNameById(project.DevelopedByCompany)
                });
            }

            return projectsView;
        }

        /// <summary>
        /// <see cref="IProjectRepository.AddPorject(Project)"/>
        /// </summary>
        public bool UpdateProject(Project project, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (project == null)
            {
                errorMessage = "Project can`t be empty";
                return false;
            }

            Project projectFromRepository = _rProjectRepository.GetAllProjects()
                .Where(p => p.Name == project.Name
                && p.DevelopedByCompany == project.DevelopedByCompany
                && p.Description == project.Description
                && p.Formula == project.Formula)
                .FirstOrDefault();

            if (projectFromRepository != null)
            {
                errorMessage = "The same project already exist";
                return false;
            }

            return _rProjectRepository.UpdateProject(project);
        }
    }
}
