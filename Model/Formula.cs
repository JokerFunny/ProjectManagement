using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    /// <summary>
    /// Model for Formula
    /// </summary>
    [Serializable]
    [DataContract]
    public class Formula
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Dictionary<Guid, int> MaterialsWithPercentQuantity { get; set; }

        [DataMember]
        public decimal WeightInGramms { get; set; }

        [DataMember]
        public Guid CreatedBy { get; set; }

        [DataMember]
        public List<Guid> SharedWith { get; set; }
    }
}
