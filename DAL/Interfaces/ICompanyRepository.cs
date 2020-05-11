using Model;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for Company repository
    /// </summary>
    public interface ICompanyRepository
    {
        /// <summary>
        /// Get all companies names
        /// </summary>
        IEnumerable<string> GetAllCompaniesNames();

        /// <summary>
        /// Get company name by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        string GetCompanyNameById(Guid id);

        /// <summary>
        /// Update company <paramref name="companyName"/>
        /// </summary>
        /// <param name="companyName">Target company name</param>
        /// <param name="description">Target description</param>
        /// <param name="countryName">Target country name</param>
        /// <param name="photo">Target photo</param>
        bool UpdateCompany(string companyName, string description, string countryName, string photo);

        /// <summary>
        /// Delete company by <paramref name="companyName"/>
        /// </summary>
        /// <param name="companyName">Target company name</param>
        bool DeleteCompany(string companyName);

        /// <summary>
        /// Gets all companies
        /// </summary>
        IEnumerable<Company> GetAllCompanies();
    }
}
