using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// Model for project
    /// </summary>
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid Formula { get; set; }

        public Guid DevelopedByCompany { get; set; }
    }
}
