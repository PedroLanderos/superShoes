using Microsoft.EntityFrameworkCore;

namespace SuperZapatos.Models
{
    public class AppDbModel :DbContext
    {
        public AppDbModel(DbContextOptions<AppDbModel> options) :base(options)
        {

        }
        // DbSet for ArticlesModel
        public DbSet<ArticlesModel> Articles { get; set; }

        // DbSet for StoresModel
        public DbSet<StoresModel> Stores { get; set; }

        // Configuraciones adicionales si lo necesitas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de las relaciones (si es necesario)

            modelBuilder.Entity<ArticlesModel>()
                .HasOne(a => a.Store)
                .WithMany(s => s.Articles)
                .HasForeignKey(a => a.StoreId)
                .OnDelete(DeleteBehavior.Cascade); // Configuración de eliminación en cascada (opcional)

            base.OnModelCreating(modelBuilder);
        }
    }
}
