using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// <see cref="ICompanyService"/>
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _rCompanyRepository;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="companyRepository"><see cref="ICompanyRepository"/></param>
        public CompanyService(ICompanyRepository companyRepository)
        {
            _rCompanyRepository = companyRepository;
        }

        /// <summary>
        /// <see cref="ICompanyService.DeleteCompany(string, out string)"/>
        /// </summary>
        public bool DeleteCompany(string companyName, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(companyName))
            {
                errorMessage = $"Company name can`t be null or empty!";
                return false;
            }

            Company company = _rCompanyRepository.GetCompanyByName(companyName);
            if (company == null)
            {
                errorMessage = $"{nameof(company)} don`t exist in Companies";
                return false;
            }

            return _rCompanyRepository.DeleteCompany(companyName);
        }

        /// <summary>
        /// <see cref="ICompanyService.GetAllCompanies"/>
        /// </summary>
        public IEnumerable<Company> GetAllCompanies()
            => _rCompanyRepository.GetAllCompanies();

        /// <summary>
        /// <see cref="ICompanyService.GetAllCompaniesNames"/>
        /// </summary>
        public IEnumerable<string> GetAllCompaniesNames()
            => _rCompanyRepository.GetAllCompaniesNames();

        /// <summary>
        /// <see cref="ICompanyService.GetCompanyNameById(Guid)"/>
        /// </summary>
        public string GetCompanyNameById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Company id can`t be empty");

            return _rCompanyRepository.GetCompanyNameById(id);
        }

        /// <summary>
        /// <see cref="ICompanyService.UpdateCompany(string, string, string, string, out string)"/>
        /// </summary>
        public bool UpdateCompany(string companyName, string description, string countryName, string photo, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(companyName))
            {
                errorMessage = "Company name can`t be null or empty!";
                return false;
            }

            var company = _rCompanyRepository.GetCompanyByName(companyName);
            if (company == null)
            {
                errorMessage = $"Company witn name {companyName} don`t exist";
                return false;
            }

            return _rCompanyRepository.UpdateCompany(companyName, description, countryName, photo);
        }
    }
}
