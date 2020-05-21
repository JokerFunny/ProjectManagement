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
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }

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
