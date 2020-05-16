using System;

namespace Model
{
    /// <summary>
    /// View model for Material
    /// </summary>
    public class MaterialViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PricePerGramm { get; set; }

        public string CreatedBy { get; set; }

        public string BannedInCountries { get; set; }
    }
}
