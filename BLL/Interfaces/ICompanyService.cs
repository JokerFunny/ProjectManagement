using Model;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for Company service
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Get all companies names
        /// </summary>
        IEnumerable<string> GetAllCompaniesNames();

        /// <summary>
        /// Get company name by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        /// <returns></returns>
        string GetCompanyNameById(Guid id);

        /// <summary>
        /// Update company <paramref name="companyName"/>
        /// </summary>
        /// <param name="companyName">Target company name</param>
        /// <param name="description">Target description</param>
        /// <param name="countryName">Target country name</param>
        /// <param name="photo">Target photo</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if update succesfull
        ///     False - if error happens
        /// </returns>
        bool UpdateCompany(string companyName, string description, string countryName, string photo, out string errorMessage);

        /// <summary>
        /// Delete company by <paramref name="companyName"/>
        /// </summary>
        /// <param name="companyName">Target company name</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if delete succesfull
        ///     False - if error happens
        /// </returns>
        bool DeleteCompany(string companyName, out string errorMessage);

        /// <summary>
        /// Gets all companies
        /// </summary>
        IEnumerable<Company> GetAllCompanies();
    }
}
