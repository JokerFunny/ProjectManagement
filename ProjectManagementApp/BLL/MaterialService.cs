using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// <see cref="IMaterialService"/>
    /// </summary>
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _rMaterialRepository;
        private readonly ICountryRepository _rCountryRepository;
        private readonly IUserRepository _rUserRepository;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="materialRepository"><see cref="IMaterialRepository"/></param>
        /// <param name="countryRepository"><see cref="ICountryRepository"/></param>
        /// <param name="userRepository"><see cref="IUserRepository"/></param>
        public MaterialService(IMaterialRepository materialRepository,
            ICountryRepository countryRepository,
            IUserRepository userRepository)
        {
            _rMaterialRepository = materialRepository;
            _rCountryRepository = countryRepository;
            _rUserRepository = userRepository;
        }

        /// <summary>
        /// <see cref="IMaterialRepository.AddMaterial(Material)"/>
        /// </summary>
        public bool AddMaterial(Material material, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (material == null)
            {
                errorMessage = "Material can`t be null";
                return false;
            }

            if (string.IsNullOrWhiteSpace(material.Name))
            {
                errorMessage = "Material name can`t be empty";
                return false;
            }

            if (material.PricePerGramm < 0)
            {
                errorMessage = "Material price can`t be negative";
                return false;
            }

            if (material.CreatedBy == Guid.Empty)
            {
                errorMessage = "Material creator can`t be emprty";
                return false;
            }

            Material materialFromRepository = _rMaterialRepository.GetAllMaterials()
                .Where(m => m.Name == material.Name && m.CreatedBy == material.CreatedBy)
                .FirstOrDefault();

            if (materialFromRepository != null)
            {
                errorMessage = "The same material already exist";
                return false;
            }

            return _rMaterialRepository.AddMaterial(material);
        }

        /// <summary>
        /// <see cref="IMaterialRepository.DeleteMaterial(Guid)"/>
        /// </summary>
        public bool DeleteMaterial(Guid id, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (id == Guid.Empty)
            {
                errorMessage = "Material id can`t be empty";
                return false;
            }

            Material materialFromRepository = _rMaterialRepository.GetMaterialById(id);

            if (materialFromRepository == null)
            {
                errorMessage = "Material don`t exist";
                return false;
            }

            return _rMaterialRepository.DeleteMaterial(id);
        }

        /// <summary>
        /// <see cref="IMaterialRepository.GetAllMaterials"/>
        /// </summary>
        public IEnumerable<Material> GetAllMaterials()
            => _rMaterialRepository.GetAllMaterials();

        /// <summary>
        /// <see cref="IMaterialService.GetCreatorByMaterialId(Guid)"/>
        /// </summary>
        public Guid GetCreatorByMaterialId(Guid materialId)
        {
            if (materialId == Guid.Empty)
                throw new ArgumentNullException("Material Id can`t be empty");

            return _rMaterialRepository.GetCreatorByMaterialId(materialId);
        }

        /// <summary>
        /// <see cref="IMaterialRepository.GetMaterialNameById(Guid)"/>
        /// </summary>
        public string GetMaterialNameById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id can`t be empty");

            return _rMaterialRepository.GetMaterialNameById(id);
        }

        /// <summary>
        /// <see cref="IMaterialRepository.UpdateMaterial(Material)"/>
        /// </summary>
        public bool UpdateMaterial(Material material, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (material == null)
            {
                errorMessage = "Material can`t be null";
                return false;
            }

            Material materialFromRepository = _rMaterialRepository.GetAllMaterials()
                .Where(m => m.Name == material.Name
                && m.Description == material.Description
                && m.PricePerGramm == material.PricePerGramm
                && m.CreatedBy == material.CreatedBy
                && m.BannedInCountries == material.BannedInCountries)
                .FirstOrDefault();

            if (materialFromRepository != null)
            {
                errorMessage = "The same material already exist";
                return false;
            }

            return _rMaterialRepository.UpdateMaterial(material);
        }

        /// <summary>
        /// <see cref="IMaterialService.GetMaterialViewModel"/>
        /// </summary>
        public IEnumerable<MaterialViewModel> GetMaterialViewModel()
        {
            var materials = _rMaterialRepository.GetAllMaterials();
            var materialsView = new List<MaterialViewModel>();

            foreach (var material in materials)
            {
                List<string> countries = new List<string>();

                var bannedCountries = material.BannedInCountries;

                if (bannedCountries != null)
                {
                    foreach (var countryId in bannedCountries)
                        countries.Add(_rCountryRepository.GetCountryNameById(countryId));
                }

                materialsView.Add(new MaterialViewModel()
                {
                    Id = material.Id,
                    Name = material.Name,
                    Description = material.Description,
                    PricePerGramm = material.PricePerGramm,
                    CreatedBy = _rUserRepository.GetUserName(material.CreatedBy),
                    BannedInCountries = string.Join(",", countries)
                });
            }

            return materialsView;
        }
    }
}
