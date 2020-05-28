using Autofac;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DB implementation of <see cref="IFormulaRepository"/>
    /// </summary>
    public class FormulaDBRepository : IFormulaRepository
    {
        private readonly ProjectManagementContext _rProjectManagementContext;
        private readonly string connString = @"Data Source=DESKTOP-HS90RUR\SQLEXPRESS;Initial Catalog=ProjectManagementDb;Integrated Security=True";

        /// <summary>
        /// Default ctor
        /// </summary>
        public FormulaDBRepository()
        {
            _rProjectManagementContext = IoC.Container.Resolve<ProjectManagementContext>();
        }

        /// <summary>
        /// <see cref="IFormulaRepository.AddFormula(Formula)"/>
        /// </summary>
        public bool AddFormula(Formula formula)
        {
            _rProjectManagementContext.Formulas.Add(formula);

            _rProjectManagementContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// <see cref="IFormulaRepository.DeleteFormula(Guid)"/>
        /// </summary>
        public bool DeleteFormula(Guid id)
        {
            try
            {
                using (var sc = new SqlConnection(connString))
                using (var cmd = sc.CreateCommand())
                {
                    sc.Open();
                    cmd.CommandText = "DELETE FROM Formulas WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// <see cref="IFormulaRepository.GetAllFormulasForUser(Guid)"/>
        /// </summary>
        public IEnumerable<Formula> GetAllFormulasForUser(Guid userId)
            => _rProjectManagementContext.Formulas
            .Where(f => f.CreatedBy == userId);

        /// <summary>
        /// <see cref="IFormulaRepository.GetAllFormulasNames"/>
        /// </summary>
        public IEnumerable<string> GetAllFormulasNames()
            => _rProjectManagementContext.Formulas
            .Select(f => f.Name);

        /// <summary>
        /// <see cref="IFormulaRepository.GetFormulaIdByName(string)"/>
        /// </summary>
        public Guid GetFormulaIdByName(string name)
            => _rProjectManagementContext.Formulas
            .Where(f => f.Name == name)
            .Select(f => f.Id)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IFormulaRepository.GetFormulaById(Guid)"/>
        /// </summary>
        public Formula GetFormulaById(Guid id)
            => _rProjectManagementContext.Formulas
            .Where(f => f.Id == id)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IFormulaRepository.GetAllFormulas"/>
        /// </summary>
        public IEnumerable<Formula> GetAllFormulas()
            => _rProjectManagementContext.Formulas;

        /// <summary>
        /// <see cref="IFormulaRepository.GetFormulaNameById(Guid)"/>
        /// </summary>
        public string GetFormulaNameById(Guid id)
            => _rProjectManagementContext.Formulas
            .Where(f => f.Id == id)
            .Select(f => f.Name)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IFormulaRepository.AddFormula(Formula)"/>
        /// </summary>
        public bool UpdateFormula(Formula formula)
        {
            var formulaFromDB = _rProjectManagementContext.Formulas.Where(f => f.Id == formula.Id).FirstOrDefault();

            if (formulaFromDB != null)
            {
                if (!string.IsNullOrWhiteSpace(formula.Name))
                    formulaFromDB.Name = formula.Name;

                if (!string.IsNullOrWhiteSpace(formula.Description))
                    formulaFromDB.Description = formula.Description;

                if (formula.WeightInGramms > 0)
                    formulaFromDB.WeightInGramms = formula.WeightInGramms;

                if (formula.MaterialsWithPercentQuantity != null)
                    formulaFromDB.MaterialsWithPercentQuantity = formula.MaterialsWithPercentQuantity;

                if (formula.SharedWith != null)
                    formulaFromDB.SharedWith = formula.SharedWith;

                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }
    }
}

