using System.Collections.Generic;

namespace RAMStorage.Serialization
{
    /// <summary>
    /// Interface for Serialization providers
    /// </summary>
    public interface ISerialize
    {
        /// <summary>
        /// Load target generic entities
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="path">Target path</param>
        /// <returns>
        ///     Deserialized data in <see cref="IList{T}"/> obj
        /// </returns>
        List<T> LoadEntities<T>(string path);

        /// <summary>
        /// Serialize target data <paramref name="entities"/> 
        /// with target <paramref name="path"/>
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="path">Target path</param>
        /// <param name="entities">Target objs</param>
        void Save<T>(string path, List<T> entities);
    }
}
