using Model;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for Material service
    /// </summary>
    public interface IMaterialService
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
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if adding succesfull
        ///     False - if error happens
        /// </returns>
        bool AddMaterial(Material material, out string errorMessage);

        /// <summary>
        /// Update material <paramref name="material"/>
        /// </summary>
        /// <param name="material">Target material</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if update succesfull
        ///     False - if error happens
        /// </returns>
        bool UpdateMaterial(Material material, out string errorMessage);

        /// <summary>
        /// Delete material by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Target id</param>
        /// <param name="errorMessage">If error happens - will contains it description</param>
        /// <returns>
        ///     True - if delete succesfull
        ///     False - if error happens
        /// </returns>
        bool DeleteMaterial(Guid id, out string errorMessage);

        /// <summary>
        /// Gets all materials
        /// </summary>
        IEnumerable<Material> GetAllMaterials();

        /// <summary>
        /// Get list of <see cref="MaterialViewModel"/>
        /// </summary>
        IEnumerable<MaterialViewModel> GetMaterialViewModel();

        /// <summary>
        /// Get material creator id by <paramref name="materialId"/>
        /// </summary>
        /// <param name="materialId">Target it</param>
        /// <returns></returns>
        Guid GetCreatorByMaterialId(Guid materialId);
    }
}
