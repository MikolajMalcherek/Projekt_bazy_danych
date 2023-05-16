using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Migrations.Operations;
using System.Linq;
using System.Collections.Generic;
using bazy.Models;

// Aby na podstawie tej klasy utworzyć bazę danych musimy utworzyć migrację, która wskaże EntityFramework wszystkie kroki, które są niezbędne
// aby odwzorować tą klasę w bazie danych (Tools -> NuGet Package Manager -> Pakcage Manager Console i wpisujemy add-migration nazwa_migracji)

namespace bazy.Data
{
    public class ZawodnikDbContext : DbContext
    {
        public virtual DbSet<Zawodnik> Zawodnicy { get; set; }
        public virtual DbSet<Miejscowosci> Miejscowosci { get; set; }
        public virtual DbSet<Wyniki> Wyniki { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // tutaj ustawiamy które kolumny są kolumanmi wymaganyi w bazie danych

            modelBuilder.Entity<Zawodnik>()
                .Property(r => r.imie_zawodnika)
                .IsRequired()
                .HasColumnName("idzawodnicy");
            modelBuilder.Entity<Zawodnik>()
                .Property(r => r.nazwisko_zawodnika)
                .IsRequired()
                .HasColumnName("nazwisko_zawodnika")
                .HasMaxLength(50);
            modelBuilder.Entity<Zawodnik>()
                .Property(r => r.imie_zawodnika)
                .IsRequired()
                .HasColumnName("imie_zawodnika")
                .HasMaxLength(50);
            modelBuilder.Entity<Zawodnik>()
                .Property(r => r.kraj_pochodzenia)
                .IsRequired()
                .HasColumnName("kraj_pochodzenia")
                .HasMaxLength(50);

            modelBuilder.Entity<Miejscowosci>()
                .Property(r => r.nazwa_miejscowosci)
                .IsRequired();

            modelBuilder.Entity<Miejscowosci>()
                .Property(r => r.kraj_miejscowosci)
                .IsRequired();
        }

        // jak powinno wyglądać połączenie do tej bazy danych
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySQL("server=127.0.0.1;port=3306;user=root;password=;database=zawody")
                .UseLoggerFactory(LoggerFactory.Create(b => b
                    .AddConsole()
                    .AddFilter(level => level >= LogLevel.Information)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
