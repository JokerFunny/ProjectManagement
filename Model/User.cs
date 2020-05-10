using System;

namespace Model
{
    /// <summary>
    /// Model for User
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public Guid CompanyId { get; set; }

        public string Photo { get; set; }
    }
}
