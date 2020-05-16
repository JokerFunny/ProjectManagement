using Model;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for Material repository
    /// </summary>
    public interface IMaterialRepository
    {
        /// <summary>
        /// Get company name by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        string GetMaterialNameById(Guid id);

        /// <summary>
        /// Add new material
        /// </summary>
        /// <param name="material">Targer material</param>
        bool AddMaterial(Material material);

        /// <summary>
        /// Update material <paramref name="material"/>
        /// </summary>
        /// <param name="material">Target material</param>
        bool UpdateMaterial(Material material);

        /// <summary>
        /// Delete material by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        bool DeleteMaterial(Guid id);

        /// <summary>
        /// Gets all materials
        /// </summary>
        IEnumerable<Material> GetAllMaterials();

        /// <summary>
        /// Get <see cref="Material"/> by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        Material GetMaterialById(Guid id);
    }
}
