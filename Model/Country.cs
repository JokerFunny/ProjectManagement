using System;

namespace Model
{
    /// <summary>
    /// Model for Country
    /// </summary>
    public class Country
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LawLink { get; set; }
    }
}
