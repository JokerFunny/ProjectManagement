using DAL.Interfaces;
using Model;
using RAMStorage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// RAM implementation of <see cref="IMaterialRepository"/>
    /// </summary>
    class MaterialRAMRepository : IMaterialRepository
    {
        /// <summary>
        /// <see cref="IMaterialRepository.AddMaterial(Material)"/>
        /// </summary>
        public bool AddMaterial(Material material)
        {
            Storage.AddMaterial(material);

            return true;
        }

        /// <summary>
        /// <see cref="IMaterialRepository.DeleteMaterial(Guid)"/>
        /// </summary>
        public bool DeleteMaterial(Guid id)
        {
            Storage.DeleteMaterial(id);

            return true;
        }

        /// <summary>
        /// <see cref="IMaterialRepository.GetAllMaterials"/>
        /// </summary>
        public IEnumerable<Material> GetAllMaterials()
            => Storage.Materials;

        /// <summary>
        /// <see cref="IMaterialRepository.GetMaterialNameById(Guid)"/>
        /// </summary>
        public string GetMaterialNameById(Guid id)
            => Storage.Materials.Where(m => m.Id == id)
            .Select(m => m.Name)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IMaterialRepository.UpdateMaterial(Material)"/>
        /// </summary>
        public bool UpdateMaterial(Material material)
        {
            Storage.UpdateMaterial(material);

            return true;
        }

        /// <summary>
        /// <see cref="IMaterialRepository.GetMaterialById(Guid)"/>
        /// </summary>
        public Material GetMaterialById(Guid id)
            => Storage.Materials.Where(m => m.Id == id)
            .FirstOrDefault();
    }
}
