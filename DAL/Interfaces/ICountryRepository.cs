using Model;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for Country repository
    /// </summary>
    public interface ICountryRepository
    {
        /// <summary>
        /// Get country name by <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        string GetCountryNameById(Guid id);

        /// <summary>
        /// Get country Id by <paramref name="name"/>
        /// </summary>
        /// <param name="name">Target name</param>
        Guid GetCountryIdByName(string name);

        /// <summary>
        /// Update country law by <paramref name="countryName"/>
        /// </summary>
        /// <param name="countryName">Target country name</param>
        /// <param name="lawLink">Target law link</param>
        bool UpdateCountryLaw(string countryName, string lawLink);

        /// <summary>
        /// Gets all countries
        /// </summary>
        IEnumerable<Country> GetAllCountries();

        /// <summary>
        /// Get <see cref="Country"/> by <paramref name="countryName"/>
        /// </summary>
        /// <param name="countryName">Target name</param>
        Country GetCountryByName(string countryName);
    }
}
