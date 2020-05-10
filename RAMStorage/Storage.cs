using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RAMStorage
{
    /// <summary>
    /// Static storage for second lab
    /// </summary>
    public static class Storage
    {
        /// <summary>
        /// List of <see cref="Country"/>
        /// </summary>
        public static List<Country> Countries { get; private set; } =
            new List<Country>() 
            { 
                new Country() { Id = Guid.NewGuid(), Name = "Ukraine", LawLink = "" }, 
                new Country() { Id = Guid.NewGuid(), Name = "Belgium", LawLink = "" },
                new Country() { Id = Guid.NewGuid(), Name = "Italy", LawLink = "" },
            };

        /// <summary>
        /// List of <see cref="Company"/>
        /// </summary>
        public static List<Company> Companies { get; private set; } =
            new List<Company>() 
            { 
                new Company() { Id = Guid.NewGuid(), Name = "TOVARIS4", Description  = "Development for U", CountryOfFoundation = Countries[0].Id, Photo = "" }, 
                new Company() { Id = Guid.NewGuid(), Name = "Sobratia", Description  = "Family company", CountryOfFoundation = Countries[1].Id, Photo = "" }, 
                new Company() { Id = Guid.NewGuid(), Name = "HimPromTorg", Description  = "Chemicals for everebody!", CountryOfFoundation = Countries[2].Id, Photo = "" }
            };

        /// <summary>
        /// Setter for <see cref="Countries"/>
        /// </summary>
        /// <param name="country">Target country</param>
        /// <exception cref="ArgumentNullException">If <paramref name="country"/> is null</exception>
        /// <exception cref="ArgumentException">
        ///     If contry with name from <paramref name="country"/> already exist in <see cref="Countries"/>
        /// </exception>
        public static void AddCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException($"{nameof(country)} can`t be null!");

            var countryFromList = Countries.FirstOrDefault(c => c.Name == country.Name);
            if (countryFromList != null)
                throw new ArgumentException($"Country witn name {country.Name} already exist");

            Countries.Add(country);
        }

        /// <summary>
        /// Update <see cref="Country.LawLink"/> for <see cref="Countries"/>
        /// </summary>
        /// <param name="countryName">Target country name</param>
        /// <param name="lawLink">Target law link</param>
        /// <exception cref="ArgumentNullException">If <paramref name="countryName"/> is null</exception>
        /// <exception cref="ArgumentNullException">
        ///     If contry with name from <paramref name="countryName"/> don`t exist in <see cref="Countries"/>
        /// </exception>
        public static void UpdateCountryLawLink(string countryName, string lawLink)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentNullException($"Contry name can`t be null or empty!");

            var countryFromList = Countries.FirstOrDefault(c => c.Name == countryName);
            if (countryFromList == null)
                throw new ArgumentNullException($"Country witn name {countryName} don`t exist");

            countryFromList.LawLink = lawLink;
        }

        /// <summary>
        /// Setter for <see cref="Companies"/>
        /// </summary>
        /// <param name="company">Target company</param>
        /// <exception cref="ArgumentNullException">If <paramref name="company"/> is null</exception>
        /// <exception cref="ArgumentException">
        ///     If company with name from <paramref name="company"/> already exist in <see cref="Companies"/>
        /// </exception>
        public static void AddCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException($"{nameof(company)} can`t be null!");

            var companyFromList = Companies.FirstOrDefault(c => c.Name == company.Name);
            if (companyFromList != null)
                throw new ArgumentException($"Country witn name {company.Name} already exist");

            Companies.Add(company);
        }

        /// <summary>
        /// Update <see cref="Company"/> for <see cref="Companies"/>
        /// </summary>
        /// <param name="companyName">Target company name</param>
        /// <param name="description">Target description</param>
        /// <param name="countryName">Target country name</param>
        /// <param name="photo">Target photo</param>
        /// <exception cref="ArgumentNullException">If <paramref name="companyName"/> is null</exception>
        /// <exception cref="ArgumentNullException">
        ///     If company with name <paramref name="companyName"/> don`t exist in <see cref="Companies"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Country with name <paramref name="countryName"/> don`t exist in <see cref="Countries"/>
        /// </exception>
        public static void UpdateCompany(string companyName, string description, string countryName, string photo)
        {
            if (string.IsNullOrWhiteSpace(companyName))
                throw new ArgumentNullException($"Company name can`t be null or empty!");

            var companyFromList = Companies.FirstOrDefault(c => c.Name == companyName);
            if (companyFromList == null)
                throw new ArgumentNullException($"Company witn name {companyName} don`t exist");

            if (!string.IsNullOrWhiteSpace(countryName))
            {
                Country country = Countries.FirstOrDefault(c => c.Name == countryName);

                if (country == null)
                    throw new ArgumentException($"Country with name {countryName} don`t exist!");

                companyFromList.CountryOfFoundation = companyFromList.Id;
            }

            companyFromList.Description = description;
            companyFromList.Photo = photo;
        }

        /// <summary>
        /// Delete company from <see cref="Companies"/>
        /// </summary>
        /// <param name="companyName">Target company name</param>
        /// <exception cref="ArgumentNullException">If <paramref name="companyName"/> is null</exception>
        /// <exception cref="ArgumentNullException">
        ///     If company with name <paramref name="companyName"/> don`t exist in <see cref="Companies"/>
        /// </exception>
        public static void DeleteCompany(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
                throw new ArgumentNullException($"Company name can`t be null or empty!");

            Company company = Companies.FirstOrDefault(c => c.Name == companyName);
            if (company == null)
                throw new ArgumentNullException($"{nameof(company)} don`t exist in Companies list");

            Companies.Remove(company);
        }
    }
}
