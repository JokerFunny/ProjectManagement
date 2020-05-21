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
    public class CompanyRAMRepository : ICompanyRepository
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
        /// <see cref="ICompanyRepository.UpdateCompany(Company)"/>
        /// </summary>
        public bool UpdateCompany(Company company)
        {
            Storage.UpdateCompany(company);

            return true;
        }

        /// <summary>
        /// <see cref="ICompanyRepository.GetCompanyIdByName(string)"/>
        /// </summary>
        public Guid GetCompanyIdByName(string companyName)
            => Storage.Companies.Where(c => c.Name == companyName).FirstOrDefault().Id;
    }
}
