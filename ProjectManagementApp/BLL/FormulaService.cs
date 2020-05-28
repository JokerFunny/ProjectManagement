using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// <see cref="IFormulaService"/>
    /// </summary>
    public class FormulaService : IFormulaService
    {
        private readonly IFormulaRepository _rFormulaRepository;
        private readonly ICompanyRepository _rCompanyRepository;
        private readonly IUserRepository _rUserRepository;
        private readonly IMaterialRepository _rMaterialRepository;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="formulaRepository"><see cref="IFormulaRepository"/></param>
        /// <param name="companyRepository"><see cref="ICompanyRepository"/></param>
        /// <param name="userRepository"><see cref="IUserRepository"/></param>
        /// <param name="materialRepository"><see cref="IMaterialRepository"/></param>
        public FormulaService(IFormulaRepository formulaRepository, 
            ICompanyRepository companyRepository,
            IUserRepository userRepository, 
            IMaterialRepository materialRepository)
        {
            _rFormulaRepository = formulaRepository;
            _rCompanyRepository = companyRepository;
            _rUserRepository = userRepository;
            _rMaterialRepository = materialRepository;
        }

        /// <summary>
        /// <see cref="IFormulaService.AddFormula(Formula, out string)"/>
        /// </summary>
        public bool AddFormula(Formula formula, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (formula == null)
            {
                errorMessage = $"{nameof(formula)} can`t be null!";
                return false;
            }

            var formulaFromRepository = _rFormulaRepository.GetAllFormulasForUser(formula.CreatedBy)
                .Where(f => f.Name == formula.Name)
                .FirstOrDefault();

            if (formulaFromRepository != null)
            {
                errorMessage = "The same formula already exist";
                return false;
            }

            return _rFormulaRepository.AddFormula(formula);
        }

        /// <summary>
        /// <see cref="IFormulaService.DeleteFormula(Guid, out string)"/>
        /// </summary>
        public bool DeleteFormula(Guid id, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (id == Guid.Empty)
            {
                errorMessage = "User id can`t be empty";
                return false;
            }

            Formula formula = _rFormulaRepository.GetFormulaById(id);
            if (formula == null)
            {
                errorMessage = $"Formula with id {id} don`t exist in Formulas list";
                return false;
            }

            return _rFormulaRepository.DeleteFormula(id);
        }

        /// <summary>
        /// <see cref="IFormulaService.GetAllFormulasForUser(Guid)"/>
        /// </summary>
        public IEnumerable<Formula> GetAllFormulasForUser(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException("User id can`t be empty");

            return _rFormulaRepository.GetAllFormulasForUser(userId);
        }

        /// <summary>
        /// <see cref="IFormulaService.GetFormulasViewModel"/>
        /// </summary>
        public IEnumerable<FormulaViewModel> GetFormulasViewModel()
        {
            var formulas = _rFormulaRepository.GetAllFormulas();
            var formulasView = new List<FormulaViewModel>();

            foreach (var formula in formulas)
            {
                List<string> companies = new List<string>();

                var shared = formula.SharedWith;

                if (shared != null)
                {
                    foreach (var companyId in shared)
                        companies.Add(_rCompanyRepository.GetCompanyNameById(companyId));
                }

                List<string> materialsWithPecrents = new List<string>();

                var materialsForFormula = formula.MaterialsWithPercentQuantity;

                if (materialsForFormula != null)
                {
                    foreach (var material in materialsForFormula)
                    {
                        var mat = _rMaterialRepository.GetMaterialById(material.Key);
                        materialsWithPecrents.Add($"{mat.Name} with price {mat.PricePerGramm} with percents {material.Value}");
                    }
                }

                formulasView.Add(new FormulaViewModel()
                {
                    Id = formula.Id,
                    Name = formula.Name,
                    Description = formula.Description,
                    MaterialsWithPercentQuantity = string.Join(", ", materialsWithPecrents),
                    WeightInGramms = formula.WeightInGramms.ToString(),
                    CreatedBy = _rUserRepository.GetUserName(formula.CreatedBy),
                    SharedWith = string.Join(", ", companies)
                });
            }

            return formulasView;
        }

        /// <summary>
        /// <see cref="IFormulaService.GetAllFormulasNames"/>
        /// </summary>
        public IEnumerable<string> GetAllFormulasNames()
            => _rFormulaRepository.GetAllFormulasNames();

        /// <summary>
        /// <see cref="IFormulaRepository.GetFormulaIdByName(string)"/>
        /// </summary>
        public Guid GetFormulaIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Formula name can`t be null or empty");

            return _rFormulaRepository.GetFormulaIdByName(name);
        }

        /// <summary>
        /// <see cref="IFormulaService.GetFormulaNameById(Guid)"/>
        /// </summary>
        public string GetFormulaNameById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Formula id can`t be empty");

            return _rFormulaRepository.GetFormulaNameById(id);
        }

        /// <summary>
        /// <see cref="IFormulaService.UpdateFormula(Formula, out string)"/>
        /// </summary>
        public bool UpdateFormula(Formula formula, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (formula == null)
            {
                errorMessage = $"{nameof(formula)} can`t be null!";
                return false;
            }

            Formula formulaFromRepository = _rFormulaRepository.GetAllFormulasForUser(formula.CreatedBy)
                .Where(f => f.Name == formula.Name)
                .FirstOrDefault();

            if (formulaFromRepository == null)
            {
                errorMessage = "Formula don`t exist!";
                return false;
            }

            return _rFormulaRepository.UpdateFormula(formula);
        }

        /// <summary>
        /// <see cref="IFormulaService.GetFormulaById(Guid)"/>
        /// </summary>
        public Formula GetFormulaById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id can`t be empty!");

            return _rFormulaRepository.GetFormulaById(id);
        }
    }
}
