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
        /// Update country law by <paramref name="countryName"/>
        /// </summary>
        /// <param name="countryName">Target country name</param>
        /// <param name="lawLink">Target law link</param>
        bool UpdateCountryLaw(string countryName, string lawLink);

        /// <summary>
        /// Gets all countries
        /// </summary>
        IEnumerable<Country> GetAllCountries();
    }
}
