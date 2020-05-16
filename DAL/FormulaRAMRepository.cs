using DAL.Interfaces;
using Model;
using RAMStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// RAM implementation of <see cref="IFormulaRepository"/>
    /// </summary>
    public class FormulaRAMRepository : IFormulaRepository
    {
        /// <summary>
        /// <see cref="IFormulaRepository.AddFormula(Formula)"/>
        /// </summary>
        public bool AddFormula(Formula formula)
        {
            Storage.AddFormula(formula);

            return true;
        }

        /// <summary>
        /// <see cref="IFormulaRepository.DeleteFormula(Guid)"/>
        /// </summary>
        public bool DeleteFormula(Guid id)
        {
            Storage.DeleteFormula(id);

            return true;
        }

        /// <summary>
        /// <see cref="IFormulaRepository.GetAllFormulasForUser(Guid)"/>
        /// </summary>
        public IEnumerable<Formula> GetAllFormulasForUser(Guid userId)
            => Storage.Formulas.Where(f => f.CreatedBy == userId);

        /// <summary>
        /// <see cref="IFormulaRepository.GetFormulaNameById(Guid)"/>
        /// </summary>
        public string GetFormulaNameById(Guid id)
            => Storage.Formulas.Where(f => f.Id == id)
            .Select(f => f.Name)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IFormulaRepository.AddFormula(Formula)"/>
        /// </summary>
        public bool UpdateFormula(Formula formula)
        {
            Storage.UpdateFormula(formula);

            return true;
        }

        /// <summary>
        /// <see cref="IFormulaRepository.GetFormulaById(Guid)"/>
        /// </summary>
        public Formula GetFormulaById(Guid id)
            => Storage.Formulas.Where(f => f.Id == id)
            .FirstOrDefault();
    }
}
