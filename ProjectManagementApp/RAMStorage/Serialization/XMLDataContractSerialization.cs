using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace RAMStorage.Serialization
{
    /// <summary>
    /// <see cref="ISerialize"/> implementation for XML Data Contract serialization
    /// </summary>
    public class XMLDataContractSerialization : ISerialize
    {
        /// <summary>
        /// <see cref="ISerialize.LoadEntities{T}(string)"/>
        /// </summary>
        public List<T> LoadEntities<T>(string path)
        {
            path += ".xml";
            List<T> list = new List<T>();

            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    DataContractSerializer ser = new DataContractSerializer(typeof(List<T>));
                    XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                    list = (List<T>)ser.ReadObject(reader);
                    reader.Close();
                }
            }

            return list;
        }

        /// <summary>
        /// <see cref="ISerialize.Save{T}(string, List{T})"/>
        /// </summary>
        public void Save<T>(string path, List<T> entities)
        {
            using (FileStream fs = new FileStream(path + ".xml", FileMode.Create))
            {
                XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
                DataContractSerializer serializator = new DataContractSerializer(typeof(List<T>));
                serializator.WriteObject(writer, entities);
                writer.Close();
            }
        }
    }
}
