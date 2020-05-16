using DAL.Interfaces;
using Model;
using RAMStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// RAM implementation of <see cref="ICompanyRepository"/>
    /// </summary>
    class CompanyRAMRepository : ICompanyRepository
    {
        /// <summary>
        /// <see cref="ICompanyRepository.DeleteCompany(string)"/>
        /// </summary>
        public bool DeleteCompany(string companyName)
        {
            Storage.DeleteCompany(companyName);

            return true;
        }

        /// <summary>
        /// <see cref="ICompanyRepository.GetAllCompanies"/>
        /// </summary>
        public IEnumerable<Company> GetAllCompanies()
            => Storage.Companies;

        /// <summary>
        /// <see cref="ICompanyRepository.GetAllCompaniesNames"/>
        /// </summary>
        public IEnumerable<string> GetAllCompaniesNames()
            => Storage.Companies.Select(c => c.Name);

        /// <summary>
        /// <see cref="ICompanyRepository.GetCompanyByName(string)"/>
        /// </summary>
        public Company GetCompanyByName(string companyName)
            => Storage.Companies.Where(c => c.Name == companyName)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="ICompanyRepository.GetCompanyNameById(Guid)"/>
        /// </summary>
        public string GetCompanyNameById(Guid id)
            => Storage.Companies.Where(c => c.Id == id)
            .Select(c => c.Name)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="ICompanyRepository.UpdateCompany(string, string, string, string)"/>
        /// </summary>
        public bool UpdateCompany(string companyName, string description, string countryName, string photo)
        {
            Storage.UpdateCompany(companyName, description, countryName, photo);

            return true;
        }
    }
}
