using Autofac;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DB implementation of <see cref="IMaterialRepository"/>
    /// </summary>
    public class MaterialDBRepository : IMaterialRepository
    {
        private readonly ProjectManagementContext _rProjectManagementContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        public MaterialDBRepository()
        {
            _rProjectManagementContext = IoC.Container.Resolve<ProjectManagementContext>();
        }

        /// <summary>
        /// <see cref="IMaterialRepository.AddMaterial(Material)"/>
        /// </summary>
        public bool AddMaterial(Material material)
        {
            _rProjectManagementContext.Materials.Add(material);

            _rProjectManagementContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// <see cref="IMaterialRepository.DeleteMaterial(Guid)"/>
        /// </summary>
        public bool DeleteMaterial(Guid id)
        {
            var material = _rProjectManagementContext.Materials.Where(m => m.Id == id).FirstOrDefault();

            if (material != null)
            {
                _rProjectManagementContext.Materials.Remove(material);
                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="IMaterialRepository.GetAllMaterials"/>
        /// </summary>
        public IEnumerable<Material> GetAllMaterials()
            => _rProjectManagementContext.Materials;

        /// <summary>
        /// <see cref="IMaterialRepository.GetMaterialNameById(Guid)"/>
        /// </summary>
        public string GetMaterialNameById(Guid id)
            => _rProjectManagementContext.Materials
            .Where(m => m.Id == id)
            .Select(m => m.Name)
            .FirstOrDefault();

        /// <summary>
        /// <see cref="IMaterialRepository.GetCreatorByMaterialId(Guid)"/>
        /// </summary>
        public Guid GetCreatorByMaterialId(Guid materialId)
            => _rProjectManagementContext.Materials
            .Where(m => m.Id == materialId)
            .FirstOrDefault().CreatedBy;

        /// <summary>
        /// <see cref="IMaterialRepository.UpdateMaterial(Material)"/>
        /// </summary>
        public bool UpdateMaterial(Material material)
        {
            var materialFromDB = _rProjectManagementContext.Materials.Where(m => m.Id == material.Id).FirstOrDefault();

            if (materialFromDB != null)
            {
                if (!string.IsNullOrWhiteSpace(material.Name))
                    materialFromDB.Name = material.Name;

                if (!string.IsNullOrWhiteSpace(material.Description))
                    materialFromDB.Description = material.Description;

                if (material.PricePerGramm > 0)
                    materialFromDB.PricePerGramm = material.PricePerGramm;

                if (material.BannedInCountries != null)
                    materialFromDB.BannedInCountries = material.BannedInCountries;

                _rProjectManagementContext.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// <see cref="IMaterialRepository.GetMaterialById(Guid)"/>
        /// </summary>
        public Material GetMaterialById(Guid id)
            => _rProjectManagementContext.Materials
            .Where(m => m.Id == id)
            .FirstOrDefault();
    }
}