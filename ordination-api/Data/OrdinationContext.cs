using Microsoft.EntityFrameworkCore;
using shared.Model;

namespace Data
{
    public class OrdinationContext : DbContext
    {
        public DbSet<Patient> Patienter => Set<Patient>();
        public DbSet<PN> PNs => Set<PN>();
        public DbSet<DagligFast> DagligFaste => Set<DagligFast>();
        public DbSet<DagligSkæv> DagligSkæve => Set<DagligSkæv>();
        public DbSet<Laegemiddel> Laegemiddler => Set<Laegemiddel>();
        public DbSet<Ordination> Ordinationer => Set<Ordination>();
        public DbSet<Dosis> Doser => Set<Dosis>(); // add this if not already there

        public OrdinationContext(DbContextOptions<OrdinationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DagligSkæv>()
                .HasMany(d => d.doser)
                .WithOne()
                .HasForeignKey("DagligSkævId");
        }
    }
}