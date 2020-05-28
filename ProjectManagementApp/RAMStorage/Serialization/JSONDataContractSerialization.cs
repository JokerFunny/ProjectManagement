using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace RAMStorage.Serialization
{
    /// <summary>
    /// <see cref="ISerialize"/> implementation for JSON Data Contract serialization
    /// </summary>
    public class JSONDataContractSerialization : ISerialize
    {
        /// <summary>
        /// <see cref="ISerialize.LoadEntities{T}(string)"/>
        /// </summary>
        public List<T> LoadEntities<T>(string path)
        {
            path += ".json";

            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    DataContractJsonSerializer reader = new DataContractJsonSerializer(typeof(List<T>));
                    List<T> list = (List<T>)reader.ReadObject(fs);
                    return list;
                }
            }

            return new List<T>();
        }

        /// <summary>
        /// <see cref="ISerialize.Save{T}(string, List{T})"/>
        /// </summary>
        public void Save<T>(string path, List<T> entities)
        {
            using (FileStream fs = new FileStream(path + ".json", FileMode.Create))
            {
                DataContractJsonSerializer writer = new DataContractJsonSerializer(typeof(List<T>));
                writer.WriteObject(fs, entities);
            }
        }
    }
}
