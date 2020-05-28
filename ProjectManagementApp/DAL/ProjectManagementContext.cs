using Model;
using System.Data.Entity;

namespace DAL
{
    /// <summary>
    /// Default implementation of <see cref="DbContext"/>
    /// </summary>
    public class ProjectManagementContext : DbContext
    {
        // Common DbSets
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Formula> Formulas { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="connectionStr">Connection string</param>
        public ProjectManagementContext(string connectionStr) 
            : base(connectionStr) 
        {
            Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ProjectManagementContext()
            : base("ProjectManagementConnectionStr")
        {
            Configuration.ProxyCreationEnabled = false;
        }
    }
}
