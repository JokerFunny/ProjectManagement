using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DB implementation of <see cref="ICompanyRepository"/>
    /// </summary>
    class CompanyDBRepository : ICompanyRepository
    {
        private readonly ProjectManagementContext _rProjectManagementContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        public CompanyDBRepository()
        {
            _rProjectManagementContext = new ProjectManagementContext();
        }

        /// <summary>
        /// <see cref="ICompanyRepository.DeleteCompany(string)"/>
        /// </summary>
        public bool DeleteCompany(string companyName)
        {
            var company = _rProjectManagementContext.Companies.Where(c => c.Name == companyName).FirstOrDefault();

            if (company != null)
            {
                _rProjectManagementContext.Companies.Remove(company);
                _rProjectManagementContext.SaveChanges();
                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="ICompanyRepository.GetAllCompanies"/>
        /// </summary>
        public IEnumerable<Company> GetAllCompanies()
            => _rProjectManagementContext.Companies;

        /// <summary>
        /// <see cref="ICompanyRepository.GetAllCompaniesNames"/>
        /// </summary>
        public IEnumerable<string> GetAllCompaniesNames()
            => _rProjectManagementContext.Companies
            .Select(c => c.Name);

        /// <summary>
        /// <see cref="ICompanyRepository.GetCompanyByName(string)"/>
        /// </summary>
        public Company GetCompanyByName(string companyName)
            => _rProjectManagementContext.Companies
            .Where(c => c.Name == companyName)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="ICompanyRepository.GetCompanyNameById(Guid)"/>
        /// </summary>
        public string GetCompanyNameById(Guid id)
            => _rProjectManagementContext.Companies
            .Where(c => c.Id == id)
            .Select(c => c.Name)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="ICompanyRepository.UpdateCompany(Company)"/>
        /// </summary>
        public bool UpdateCompany(Company company)
        {
            var companyFromRepo = _rProjectManagementContext.Companies.Where(c => c.Id == company.Id).FirstOrDefault();

            if (companyFromRepo != null)
            {
                companyFromRepo.Description = company.Description ?? companyFromRepo.Description;
                companyFromRepo.Photo = company.Photo ?? companyFromRepo.Photo;
                companyFromRepo.CountryOfFoundation = company.CountryOfFoundation != Guid.Empty ? company.CountryOfFoundation : companyFromRepo.CountryOfFoundation;

                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="ICompanyRepository.GetCompanyIdByName(string)"/>
        /// </summary>
        public Guid GetCompanyIdByName(string companyName)
            => _rProjectManagementContext.Companies
            .Where(c => c.Name == companyName)
            .FirstOrDefault().Id;
    }
}
