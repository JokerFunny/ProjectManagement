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
        /// Get formula id by <paramref name="name"/>
        /// </summary>
        /// <param name="name">Target name</param>
        Guid GetFormulaIdByName(string name);

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

        /// <summary>
        /// Get all <see cref="Formula.Name"/>
        /// </summary>
        IEnumerable<string> GetAllFormulasNames();

        /// <summary>
        /// Get <see cref="Formula"/> by <paramref name="Id"/>
        /// </summary>
        /// <param name="Id">Target Id</param>
        Formula GetFormulaById(Guid Id);
    }
}
