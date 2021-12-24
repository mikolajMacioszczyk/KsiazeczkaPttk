using KsiazeczkaPttk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KsiazeczkaPttk.DAL
{
    public class KsiazeczkaContext : DbContext
    {
        public KsiazeczkaContext(DbContextOptions<KsiazeczkaContext> options) : base(options)
        {
        }

        public DbSet<GotPttk> GotPttk { get; set; }
        public DbSet<PosiadanieGotPttk> PosiadaneGotPttk { get; set; }
        public DbSet<Ksiazeczka> Ksiazeczki { get; set; }
        public DbSet<RolaUzytkownika> RoleUzytkownikow { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<StatusWycieczki> StatusyWycieczek { get; set; }
        public DbSet<Wycieczka> Wycieczki { get; set; }
        public DbSet<Weryfikacje> Weryfikacje { get; set; }
        public DbSet<PunktTerenowy> PunktyTerenowe { get; set; }
        public DbSet<GrupaGorska> GrupyGorskie { get; set; }
        public DbSet<PasmoGorskie> PasmaGorskie { get; set; }
        public DbSet<Odcinek> Odcinki { get; set; }
        public DbSet<ZamkniecieOdcinka> ZamknieciaOdcinkow { get; set; }
        public DbSet<TypPotwierdzeniaTerenowego> TypyPotwierdzenTerenowych { get; set; }
        public DbSet<PotwierdzenieTerenowe> PotwierdzeniaTerenowe { get; set; }
        public DbSet<PrzebycieOdcinka> PrzebyteOdcinki { get; set; }
        public DbSet<PotwierdzenieTerenowePrzebytegoOdcinka> PotwierdzeniaTerenowePrzebytychOdcinkow { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GotPttk>()
                .HasIndex(x => x.Poziom)
                .IsUnique(true);

            modelBuilder.Entity<PosiadanieGotPttk>()
                .HasKey(x => new { x.Wlasciciel, x.Odznaka });

            modelBuilder.Entity<PunktTerenowy>()
                .HasIndex(x => x.Nazwa)
                .IsUnique(true);

            modelBuilder.Entity<GrupaGorska>()
                .HasIndex(x => x.Nazwa)
                .IsUnique(true);

            modelBuilder.Entity<PasmoGorskie>()
                .HasIndex(x => x.Nazwa)
                .IsUnique(true);

            modelBuilder.Entity<ZamkniecieOdcinka>()
               .HasKey(x => new { x.OdcinekId, x.DataZamkniecia });

            base.OnModelCreating(modelBuilder);
        }
    }
}
