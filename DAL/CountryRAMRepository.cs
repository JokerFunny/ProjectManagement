using DAL.Interfaces;
using Model;
using RAMStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// RAM implementation of <see cref="ICountryRepository"/>
    /// </summary>
    public class CountryRAMRepository : ICountryRepository
    {
        /// <summary>
        /// <see cref="ICountryRepository.GetAllCountries"/>
        /// </summary>
        public IEnumerable<Country> GetAllCountries()
            => Storage.Countries;

        /// <summary>
        /// <see cref="ICountryRepository.GetCountryIdByName(string)"/>
        /// </summary>
        public Guid GetCountryIdByName(string name)
            => Storage.Countries.Where(c => c.Name == name)
            .FirstOrDefault().Id;

        /// <summary>
        /// <see cref="ICountryRepository.GetCountryNameById(Guid)"/>
        /// </summary>
        public string GetCountryNameById(Guid id)
            => Storage.Countries.Where(c => c.Id == id)
            .Select(c => c.Name)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="ICountryRepository.UpdateCountryLaw(string, string)"/>
        /// </summary>
        public bool UpdateCountryLaw(string countryName, string lawLink)
        {
            Storage.UpdateCountryLawLink(countryName, lawLink);

            return true;
        }

        /// <summary>
        /// <see cref="ICountryRepository.GetCountryByName(string)"/>
        /// </summary>
        public Country GetCountryByName(string countryName)
            => Storage.Countries.Where(c => c.Name == countryName)
            .FirstOrDefault();
    }
}
