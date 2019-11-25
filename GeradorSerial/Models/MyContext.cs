using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GeradorSerial.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MyContext : DbContext
    {
        public MyContext() : base("name=Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove unused conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Add entity configurations in a structured way using 'TypeConfiguration’ classes
            //modelBuilder.Configurations.Add(new ExemploEntityConfiguration());
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Chave> Chaves { get; set; }

        public DbSet<MasterKey> MasterKeys { get; set; }
    }
}
