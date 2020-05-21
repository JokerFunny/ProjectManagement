using System;
using System.Runtime.Serialization;

namespace Model
{
    /// <summary>
    /// Model for Company
    /// </summary>
    [Serializable]
    [DataContract]
    public class Company
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Guid CountryOfFoundation { get; set; }

        [DataMember]
        public string Photo { get; set; }
    }
}
