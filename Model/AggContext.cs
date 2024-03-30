using Microsoft.EntityFrameworkCore;

namespace AgeenkovApp.Model {
    public class AggContext : DbContext {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<AreaCoords> AreaCoords { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileCoords> ProfileCoords { get; set; }
        public DbSet<Picket> Pickets { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Measuring> Measurings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer($@"Server=.;Database=AggAppNew;Trusted_Connection=True;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AgeenkovAppNew;Trusted_Connection=True;");
        }
    }    
}
