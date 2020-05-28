using System;

namespace Model
{
    /// <summary>
    /// Model for new user
    /// </summary>
    public class NewUser
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid CompanyId { get; set; }

        public string Photo { get; set; }
    }
}
