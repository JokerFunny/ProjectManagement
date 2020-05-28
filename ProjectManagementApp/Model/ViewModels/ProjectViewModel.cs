using System;

namespace Model
{
    /// <summary>
    /// View model for project
    /// </summary>
    public class ProjectViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string FormulaName { get; set; }

        public decimal Weight { get; set; }

        public decimal TotalPrice { get; set; }

        public string DevelopedByCompany { get; set; }
    }
}
