using Model;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for Formula repository
    /// </summary>
    public interface IFormulaRepository
    {
        /// <summary>
        /// Get formula name by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        string GetFormulaNameById(Guid id);

        /// <summary>
        /// Add new formula
        /// </summary>
        /// <param name="formula">Targer formula</param>
        bool AddFormula(Formula formula);

        /// <summary>
        /// Update formula <paramref name="formula"/>
        /// </summary>
        /// <param name="formula">Target formula</param>
        bool UpdateFormula(Formula formula);

        /// <summary>
        /// Delete formula by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        bool DeleteFormula(Guid id);

        /// <summary>
        /// Gets formulas, satisfied for company politics
        /// </summary>
        /// <param name="userId">User id</param>
        IEnumerable<Formula> GetAllFormulasForUser(Guid userId);
    }
}
