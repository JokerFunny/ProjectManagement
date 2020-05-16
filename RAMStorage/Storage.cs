using Infrastructure;
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
        public static Guid CurrentUser { get; set; }

        /// <summary>
        /// List of <see cref="Country"/>
        /// </summary>
        public static List<Country> Countries { get; private set; }

        /// <summary>
        /// List of <see cref="Company"/>
        /// </summary>
        public static List<Company> Companies { get; private set; }

        /// <summary>
        /// List of <see cref="User"/>
        /// </summary>
        public static List<User> Users { get; private set; }

        /// <summary>
        /// List of <see cref="Material"/>
        /// </summary>
        public static List<Material> Materials { get; private set; }

        /// <summary>
        /// List of <see cref="Formula"/>
        /// </summary>
        public static List<Formula> Formulas { get; private set; }

        /// <summary>
        /// List of <see cref="Project"/>
        /// </summary>
        public static List<Project> Projects { get; private set; }

        static Storage()
        {
            CurrentUser = Guid.Empty;

            Countries = new List<Country>()
            {
                new Country() { Id = Guid.NewGuid(), Name = "Ukraine", LawLink = "" },
                new Country() { Id = Guid.NewGuid(), Name = "Belgium", LawLink = "" },
                new Country() { Id = Guid.NewGuid(), Name = "Italy", LawLink = "" },
            };

            Companies = new List<Company>()
            {
                new Company() { Id = Guid.NewGuid(), Name = "TOVARIS4", Description  = "Development for U", CountryOfFoundation = Countries[0].Id, Photo = "" },
                new Company() { Id = Guid.NewGuid(), Name = "Sobratia", Description  = "Family company", CountryOfFoundation = Countries[1].Id, Photo = "" },
                new Company() { Id = Guid.NewGuid(), Name = "HimPromTorg", Description  = "Chemicals for everebody!", CountryOfFoundation = Countries[2].Id, Photo = "" }
            };

            byte[] passwordSalt = PasswordHelper.GenerateSalt();
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "Emai123@ea.com",
                CompanyId = Companies[0].Id,
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                PasswordHash = PasswordHelper.HashPassword("Testpass123", passwordSalt),
                Photo = ""
            };

            Users = new List<User>() { user };

            Materials = new List<Material>()
            {
                new Material() { Id = Guid.NewGuid(), Name = "Watter", Description  = "Simple H2O", PricePerGramm = 0.01M, BannedInCountries = new List<Guid>() { Countries[2].Id }, CreatedBy = Users[0].Id, Image = "" },
                new Material() { Id = Guid.NewGuid(), Name = "Salt", Description  = "Simple salt", PricePerGramm = 0.35M, BannedInCountries = null, CreatedBy = Users[0].Id, Image = "" }
            };

            Formulas = new List<Formula>()
            {
                new Formula()
                {
                    Id = Guid.NewGuid(),
                    Name = "Simple formula",
                    Description  = "Test formula",
                    CreatedBy = Users[0].Id,
                    SharedWith = new List<Guid>() { Companies[2].Id },
                    MaterialsWithPercentQuantity = new Dictionary<Guid, int>()
                    {
                        { Materials[0].Id, 90 },
                        { Materials[1].Id, 10 }
                    },
                    WeightInGramms = 100M
                }
            };

            Projects = new List<Project>()
            {
                new Project() { Id = Guid.NewGuid(), Name = "Test proj", Description  = "Test proj 1", DevelopedByCompany = Users[0].CompanyId, Formula = Formulas[0].Id }
            };
        }

        #region Country methods

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
        /// Update <see cref="Country.LawLink"/> by <paramref name="countryName"/> for <see cref="Countries"/>
        /// </summary>
        /// <param name="countryName">Target country name</param>
        /// <param name="lawLink">Target law link</param>
        /// <exception cref="ArgumentNullException">If <paramref name="countryName"/> is null</exception>
        /// <exception cref="ArgumentNullException">
        ///     If contry with name from <paramref name="countryName"/> don`t exist in <see cref="Countries"/>
        /// </exception>
        public static void UpdateCountryLawLink(string countryName, string lawLink = null)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentNullException($"Contry name can`t be null or empty!");

            var countryFromList = Countries.FirstOrDefault(c => c.Name == countryName);

            countryFromList.LawLink = lawLink;
        }

        #endregion

        #region Company methods

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
        /// Update <see cref="Company"/> by <paramref name="companyName"/> for <see cref="Companies"/>
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
        public static void UpdateCompany(string companyName, string description = null, string countryName = null, string photo = null)
        {
            if (string.IsNullOrWhiteSpace(companyName))
                throw new ArgumentNullException($"Company name can`t be null or empty!");

            var companyFromList = Companies.FirstOrDefault(c => c.Name == companyName);

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

            Companies.Remove(company);
        }

        #endregion

        #region User methods

        /// <summary>
        /// Setter for <see cref="Users"/>
        /// </summary>
        /// <param name="user">Target user</param>
        /// <exception cref="ArgumentNullException">If <paramref name="user"/> is null</exception>
        public static void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException($"{nameof(user)} can`t be null!");

            Users.Add(user);
        }

        /// <summary>
        /// Update <see cref="User"/> by email for <see cref="Users"/>
        /// </summary>
        /// <param name="newUser">Target user</param>
        /// <exception cref="ArgumentNullException">If <paramref name="newUser.Email"/> is null</exception>
        /// <exception cref="ArgumentNullException">
        ///     If company with name <paramref name="companyName"/> don`t exist in <see cref="Companies"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Country with name <paramref name="countryName"/> don`t exist in <see cref="Countries"/>
        /// </exception>
        public static void UpdateUser(User newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Email))
                throw new ArgumentNullException($"Email can`t be null or empty!");

            User userFromList = Users.FirstOrDefault(u => u.Email == newUser.Email);

            userFromList.FirstName = newUser.FirstName;
            userFromList.LastName = newUser.LastName;
            userFromList.PasswordSalt = newUser.PasswordSalt;
            userFromList.PasswordHash = newUser.PasswordHash;
            userFromList.Photo = newUser.Photo;
        }

        /// <summary>
        /// Delete user from <see cref="Users"/>
        /// </summary>
        /// <param name="userEmail">Target user email</param>
        /// <exception cref="ArgumentNullException">If <paramref name="userEmail"/> is null</exception>
        public static void DeleteUser(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new ArgumentNullException($"User email can`t be null or empty!");

            User user = Users.FirstOrDefault(u => u.Email == userEmail);

            Users.Remove(user);
        }

        #endregion

        #region Material methods

        /// <summary>
        /// Setter for <see cref="Materials"/>
        /// </summary>
        /// <param name="material">Target material</param>
        /// <exception cref="ArgumentNullException">If <paramref name="material"/> is null</exception>
        /// <exception cref="ArgumentException">
        ///     If same material already exist in <see cref="Materials"/>
        /// </exception>
        public static void AddMaterial(Material material)
        {
            if (material == null)
                throw new ArgumentNullException($"{nameof(material)} can`t be null!");

            var materialFromList = Materials.FirstOrDefault(c => c.Equals(material));

            Materials.Add(material);
        }

        /// <summary>
        /// Update <see cref="Material"/> for <see cref="Materials"/>
        /// </summary>
        /// <param name="material">Target material</param>
        /// <exception cref="ArgumentNullException">If <paramref name="material"/> is null</exception>
        /// <exception cref="ArgumentException">
        ///     If same material already exist in <see cref="Materials"/>
        /// </exception>
        public static void UpdateMaterial(Material material)
        {
            if (material == null)
                throw new ArgumentNullException($"{nameof(material)} can`t be null");

            var materialFromList = Materials.FirstOrDefault(m => m.Equals(material));

            materialFromList.Name = material.Name ?? materialFromList.Name;
            materialFromList.Description = material.Description ?? materialFromList.Description;
            materialFromList.BannedInCountries = material.BannedInCountries ?? materialFromList.BannedInCountries;
            materialFromList.Image = material.Image ?? materialFromList.Image;
        }

        /// <summary>
        /// Delete <see cref="Material"/> by <paramref name="id"/> from <see cref="Materials"/>
        /// </summary>
        /// <param name="id">Target id</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is empty</exception>
        /// <exception cref="ArgumentNullException">
        ///     If material with id <paramref name="id"/> don`t exist in <see cref="Materials"/>
        /// </exception>
        public static void DeleteMaterial(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException($"Target id can`t be empty");

            Material material = Materials.FirstOrDefault(m => m.Id == id);

            Materials.Remove(material);
        }

        #endregion

        #region Formula methods

        /// <summary>
        /// Setter for <see cref="Formulas"/>
        /// </summary>
        /// <param name="formula">Target formula</param>
        /// <exception cref="ArgumentNullException">If <paramref name="formula"/> is null</exception>
        /// <exception cref="ArgumentException">
        ///     If same formula already exist in <see cref="Formulas"/>
        /// </exception>
        public static void AddFormula(Formula formula)
        {
            if (formula == null)
                throw new ArgumentNullException($"{nameof(formula)} can`t be null!");

            var formulaFromList = Formulas.FirstOrDefault(c => c.Equals(formula));

            Formulas.Add(formula);
        }

        /// <summary>
        /// Update <see cref="Formula"/> for <see cref="Formulas"/>
        /// </summary>
        /// <param name="formula">Target formula</param>
        /// <exception cref="ArgumentNullException">If <paramref name="formula"/> is null</exception>
        /// <exception cref="ArgumentException">
        ///     If same formula already exist in <see cref="Formulas"/>
        /// </exception>
        public static void UpdateFormula(Formula formula)
        {
            if (formula == null)
                throw new ArgumentNullException($"{nameof(formula)} can`t be null");

            // override Equals and GetHashCode for Formula
            var formulaFromList = Formulas.FirstOrDefault(f => f.Equals(formula));

            formulaFromList.Name = formula.Name ?? formulaFromList.Name;
            formulaFromList.Description = formula.Description ?? formulaFromList.Description;
            formulaFromList.MaterialsWithPercentQuantity = formula.MaterialsWithPercentQuantity ?? formulaFromList.MaterialsWithPercentQuantity;
            formulaFromList.SharedWith = formula.SharedWith ?? formulaFromList.SharedWith;
            formulaFromList.WeightInGramms = formula.WeightInGramms > 0 ? formula.WeightInGramms : formulaFromList.WeightInGramms;
        }

        /// <summary>
        /// Delete <see cref="Formulas"/> by <paramref name="id"/> from <see cref="Formulas"/>
        /// </summary>
        /// <param name="id">Target id</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is empty</exception>
        /// <exception cref="ArgumentNullException">
        ///     If formula with id <paramref name="id"/> don`t exist in <see cref="Formulas"/>
        /// </exception>
        public static void DeleteFormula(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException($"Target id can`t be empty");

            Formula formula = Formulas.FirstOrDefault(f => f.Id == id);

            Formulas.Remove(formula);
        }

        #endregion

        #region Project methods

        /// <summary>
        /// Setter for <see cref="Projects"/>
        /// </summary>
        /// <param name="project">Target project</param>
        /// <exception cref="ArgumentNullException">If <paramref name="project"/> is null</exception>
        /// <exception cref="ArgumentException">
        ///     If same project already exist in <see cref="Projects"/>
        /// </exception>
        public static void AddProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException($"{nameof(project)} can`t be null!");

            var projectFromList = Projects.FirstOrDefault(p => p.Equals(project));

            Projects.Add(project);
        }

        /// <summary>
        /// Update <see cref="Project"/> for <see cref="Projects"/>
        /// </summary>
        /// <param name="project">Target project</param>
        /// <exception cref="ArgumentNullException">If <paramref name="project"/> is null</exception>
        /// <exception cref="ArgumentException">
        ///     If same project already exist in <see cref="Projects"/>
        /// </exception>
        public static void UpdateProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException($"{nameof(project)} can`t be null");

            var projectFromList = Projects.FirstOrDefault(p => p.Equals(project));

            projectFromList.Name = project.Name ?? projectFromList.Name;
            projectFromList.Description = project.Description ?? projectFromList.Description;
            projectFromList.Formula = project.Formula != Guid.Empty ? project.Formula : projectFromList.Formula;
        }

        /// <summary>
        /// Delete <see cref="Project"/> by <paramref name="id"/> from <see cref="Projects"/>
        /// </summary>
        /// <param name="id">Target id</param>
        /// <exception cref="ArgumentNullException">If <paramref name="id"/> is empty</exception>
        /// <exception cref="ArgumentNullException">
        ///     If project with id <paramref name="id"/> don`t exist in <see cref="Projects"/>
        /// </exception>
        public static void DeleteProject(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException($"Target id can`t be empty");

            Project project = Projects.FirstOrDefault(p => p.Id == id);

            Projects.Remove(project);
        }

        #endregion
    }
}
