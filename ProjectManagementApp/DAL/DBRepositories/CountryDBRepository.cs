using Autofac;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DB implementation of <see cref="ICountryRepository"/>
    /// </summary>
    public class CountryDBRepository : ICountryRepository
    {
        private readonly ProjectManagementContext _rProjectManagementContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        public CountryDBRepository()
        {
            _rProjectManagementContext = IoC.Container.Resolve<ProjectManagementContext>();
        }

        /// <summary>
        /// <see cref="ICountryRepository.GetAllCountries"/>
        /// </summary>
        public IEnumerable<Country> GetAllCountries()
            => _rProjectManagementContext.Countries;

        /// <summary>
        /// <see cref="ICountryRepository.GetCountryIdByName(string)"/>
        /// </summary>
        public Guid GetCountryIdByName(string name)
            => _rProjectManagementContext.Countries
            .Where(c => c.Name == name)
            .FirstOrDefault().Id;

        /// <summary>
        /// <see cref="ICountryRepository.GetCountryNameById(Guid)"/>
        /// </summary>
        public string GetCountryNameById(Guid id)
            => _rProjectManagementContext.Countries.Where(c => c.Id == id)
            .Select(c => c.Name)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="ICountryRepository.UpdateCountryLaw(string, string)"/>
        /// </summary>
        public bool UpdateCountryLaw(string countryName, string lawLink)
        {
            var country = _rProjectManagementContext.Countries.Where(c => c.Name == countryName).FirstOrDefault();

            if (country != null)
            {
                if (!string.IsNullOrWhiteSpace(lawLink))
                    country.LawLink = lawLink;

                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="ICountryRepository.GetCountryByName(string)"/>
        /// </summary>
        public Country GetCountryByName(string countryName)
            => _rProjectManagementContext.Countries
            .Where(c => c.Name == countryName)
            .FirstOrDefault();
    }
}
