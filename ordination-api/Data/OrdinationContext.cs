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
        public DbSet<Dosis> Doser => Set<Dosis>(); 

        public OrdinationContext(DbContextOptions<OrdinationContext> options) : base(options) { }

        //EF kunne ikke automatisk gemme vores dosis, så vi tilføjer denne kode for at fortælle den hvordan det skal gemmes og hvad det skal linkes sammen med
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DagligSkæv>()
                .HasMany(d => d.doser)
                .WithOne()
                .HasForeignKey("DagligSkævId");
        }
    }
}