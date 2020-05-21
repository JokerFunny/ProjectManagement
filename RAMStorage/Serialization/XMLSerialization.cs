using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace RAMStorage.Serialization
{
    /// <summary>
    /// <see cref="ISerialize"/> implementation for XML serialization
    /// </summary>
    public class XMLSerialization : ISerialize
    {
        /// <summary>
        /// <see cref="ISerialize.LoadEntities{T}(string)"/>
        /// </summary>
        public List<T> LoadEntities<T>(string path)
        {
            path += ".xml";

            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
                    List<T> list = (List<T>)formatter.Deserialize(fs);
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
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
                formatter.Serialize(fs, entities);
            }
        }
    }
}
