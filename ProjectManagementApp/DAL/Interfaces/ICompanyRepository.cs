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
        /// Update company <paramref name="company"/>
        /// </summary>
        /// <param name="company">Target company</param>
        bool UpdateCompany(Company company);

        /// <summary>
        /// Delete company by <paramref name="companyName"/>
        /// </summary>
        /// <param name="companyName">Target company name</param>
        bool DeleteCompany(string companyName);

        /// <summary>
        /// Gets all companies
        /// </summary>
        IEnumerable<Company> GetAllCompanies();

        /// <summary>
        /// Get company by <paramref name="companyName"/>
        /// </summary>
        /// <param name="companyName">Target company name</param>
        Company GetCompanyByName(string companyName);

        /// <summary>
        /// Get company id by <paramref name="companyName"/>
        /// </summary>
        /// <param name="companyName">Target name</param>
        Guid GetCompanyIdByName(string companyName);
    }
}
