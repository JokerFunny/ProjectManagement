using System;
using System.Runtime.Serialization;

namespace Model
{
    /// <summary>
    /// Model for project
    /// </summary>
    [Serializable]
    [DataContract]
    public class Project
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Guid Formula { get; set; }

        [DataMember]
        public Guid DevelopedByCompany { get; set; }
    }
}
