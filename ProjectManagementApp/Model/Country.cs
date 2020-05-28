using System;
using System.Runtime.Serialization;

namespace Model
{
    /// <summary>
    /// Model for Country
    /// </summary>
    [Serializable]
    [DataContract]
    public class Country
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string LawLink { get; set; }
    }
}
