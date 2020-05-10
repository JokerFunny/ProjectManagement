using System;

namespace Model
{
    /// <summary>
    /// Model for Company
    /// </summary>
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid CountryOfFoundation { get; set; }

        public string Photo { get; set; }
    }
}
