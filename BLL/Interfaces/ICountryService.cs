using Model;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for Country service
    /// </summary>
    public interface ICountryService
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
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if update succesfull
        ///     False - if error happens
        /// </returns>
        bool UpdateCountryLaw(string countryName, string lawLink, out string errorMessage);

        /// <summary>
        /// Gets all countries
        /// </summary>
        IEnumerable<Country> GetAllCountries();
    }
}
