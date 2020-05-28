using System;

namespace Model
{
    /// <summary>
    /// View model for Formula
    /// </summary>
    public class FormulaViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MaterialsWithPercentQuantity { get; set; }

        public string WeightInGramms { get; set; }

        public string CreatedBy { get; set; }

        public string SharedWith { get; set; }
    }
}
