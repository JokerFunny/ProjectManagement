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

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="materialRepository"><see cref="IMaterialRepository"/></param>
        public MaterialService(IMaterialRepository materialRepository)
        {
            _rMaterialRepository = materialRepository;
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
                && m.CreatedBy == material.CreatedBy)
                .FirstOrDefault();

            if (materialFromRepository != null)
            {
                errorMessage = "The same material already exist";
                return false;
            }

            return _rMaterialRepository.UpdateMaterial(material);
        }
    }
}
