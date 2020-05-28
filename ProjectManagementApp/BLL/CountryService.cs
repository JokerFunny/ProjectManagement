using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// <see cref="ICountryService"/>
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _rCountryRepository;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="countryRepository"><see cref="ICountryRepository"/></param>
        public CountryService(ICountryRepository countryRepository)
        {
            _rCountryRepository = countryRepository;
        }

        /// <summary>
        /// <see cref="ICountryService.GetAllCountries"/>
        /// </summary>
        public IEnumerable<Country> GetAllCountries()
            => _rCountryRepository.GetAllCountries();

        /// <summary>
        /// <see cref="ICountryService.GetCountryNameById(Guid)"/>
        /// </summary>
        public string GetCountryNameById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Country id can`t be empty!");

            return _rCountryRepository.GetCountryNameById(id);
        }

        /// <summary>
        /// <see cref="ICountryService.GetCountryIdByName(string)"/>
        /// </summary>
        public Guid GetCountryIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Country naem can`t be null or empty!");

            return _rCountryRepository.GetCountryIdByName(name);
        }

        /// <summary>
        /// <see cref="ICountryService.UpdateCountryLaw(string, string, out string)"/>
        /// </summary>
        public bool UpdateCountryLaw(string countryName, string lawLink, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(countryName))
            {
                errorMessage = "Country name can`t be null or empty!";
                return false;
            }

            var country = _rCountryRepository.GetCountryByName(countryName);
            if (country == null)
            {
                errorMessage = $"Country witn name {countryName} don`t exist";
                return false;
            }

            return _rCountryRepository.UpdateCountryLaw(countryName, lawLink);
        }
    }
}
