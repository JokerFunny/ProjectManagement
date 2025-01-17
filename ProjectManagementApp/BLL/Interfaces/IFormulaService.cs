﻿using Model;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for Formula service
    /// </summary>
    public interface IFormulaService
    {
        /// <summary>
        /// Get formula name by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        string GetFormulaNameById(Guid id);

        /// <summary>
        /// Get formula id by <paramref name="name"/>
        /// </summary>
        /// <param name="name">Target name</param>
        Guid GetFormulaIdByName(string name);

        /// <summary>
        /// Add new formula
        /// </summary>
        /// <param name="formula">Targer formula</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if adding succesfull
        ///     False - if error happens
        /// </returns>
        bool AddFormula(Formula formula, out string errorMessage);

        /// <summary>
        /// Update formula <paramref name="formula"/>
        /// </summary>
        /// <param name="formula">Target formula</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if update succesfull
        ///     False - if error happens
        /// </returns>
        bool UpdateFormula(Formula formula, out string errorMessage);

        /// <summary>
        /// Delete formula by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if delete succesfull
        ///     False - if error happens
        /// </returns>
        bool DeleteFormula(Guid id, out string errorMessage);

        /// <summary>
        /// Get <see cref="Formula"/> by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        Formula GetFormulaById(Guid id);

        /// <summary>
        /// Gets formulas, satisfied for company politics
        /// </summary>
        /// <param name="userId">User id</param>
        IEnumerable<Formula> GetAllFormulasForUser(Guid userId);

        /// <summary>
        /// Get list of <see cref="FormulaViewModel"/>
        /// </summary>
        IEnumerable<FormulaViewModel> GetFormulasViewModel();

        /// <summary>
        /// Get all <see cref="Formula.Name"/>
        /// </summary>
        IEnumerable<string> GetAllFormulasNames();
    }
}
