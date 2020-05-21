using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    /// <summary>
    /// Model for Material
    /// </summary>
    [Serializable]
    [DataContract]
    public class Material
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int PricePerGramm { get; set; }

        [DataMember]
        public Guid CreatedBy { get; set; }

        [DataMember]
        public List<Guid> BannedInCountries { get; set; }
    }
}
