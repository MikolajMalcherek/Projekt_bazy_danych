
// tutaj bedziemy dodawac pola w tabeli, ktore zawsze musza sie w niej znajdowac, np. Mikolaj Malchereke i Jakub Jankowiak
using bazy.Models;
using bazy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy
{
    public class ZawodnikSeeder
    {
        private readonly ZawodnikDbContext _dbContext;

        public ZawodnikSeeder(ZawodnikDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                Console.WriteLine("DODAWANIE");
                if (!_dbContext.Zawodnicy.Any())
                {
                    var zawodnicy = GetZawodnicy();
                    _dbContext.Zawodnicy.AddRange(zawodnicy);
                    // Zapisujemy zmiany na kontekscie bazy danych
                    _dbContext.SaveChanges();
                }
            }
            else Console.WriteLine("NIE MA POLACZENIA");
        }

        private IEnumerable<Zawodnik> GetZawodnicy()
        {
            var zawodnicy = new List<Zawodnik>()
            {
            new Zawodnik()
            {
                imie_zawodnika = "Mikolaj",
                nazwisko_zawodnika = "Malcherek",
                kraj_pochodzenia = "Polska",
            },
            new Zawodnik()
            {
                imie_zawodnika = "Jakub",
                nazwisko_zawodnika = "Jankowiak",
                kraj_pochodzenia = "Polska"
            }
            };
            return zawodnicy;
        }
    }
}
