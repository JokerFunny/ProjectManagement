using System;
using System.Runtime.Serialization;

namespace Model
{
    /// <summary>
    /// Model for User
    /// </summary>
    [Serializable]
    [DataContract]
    public class User
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        public string PasswordSalt { get; set; }

        [DataMember]
        public Guid CompanyId { get; set; }

        [DataMember]
        public string Photo { get; set; }
    }
}
