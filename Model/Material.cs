using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Model for Material
    /// </summary>
    public class Material
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal PricePerGramm { get; set; }

        public string Image { get; set; }

        public Guid CreatedBy { get; set; }

        public List<Guid> BannedInCountries { get; set; }
    }
}
