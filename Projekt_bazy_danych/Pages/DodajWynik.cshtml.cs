using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using bazy.Data;
using bazy.Models;
using Microsoft.EntityFrameworkCore;

namespace bazy.Pages
{
    public class DodajWynikModel : PageModel
    {
        private bazy.Data.ZawodnikDbContext _context;

        public DodajWynikModel(bazy.Data.ZawodnikDbContext context)
        {
            _context = context;
        }

        //public IActionResult OnGet()
        //{
        //    return Page();
        //}

        [BindProperty]
        public Zawodnik Zawodnik { get; set; } = default!;

        [BindProperty]
        public int wynik { get; set; }
        [BindProperty]
        public Miejscowosci Miejscowosc { get; set; }

        public Wyniki Wyniki { get; set; }

        public async Task OnPostAsync()
        {
            var dbContext = new ZawodnikDbContext();
            Wyniki = new Wyniki();


            // Nie ma takiej osoby
            if (dbContext.Zawodnicy.Where(o => o.imie_zawodnika == Zawodnik.imie_zawodnika && o.nazwisko_zawodnika == Zawodnik.nazwisko_zawodnika).Count() == 0)
            {
                ViewData["Message"] = $"Nie znaleziono osoby o imieniu {Zawodnik.imie_zawodnika} {Zawodnik.nazwisko_zawodnika}.";
            }

            // Nie ma takiej miejscowosci zawodów
            else if (dbContext.Miejscowosci.Where(o => o.nazwa_miejscowosci == Miejscowosc.nazwa_miejscowosci).Count() == 0)
            {
                ViewData["Message"] = $"Nie znaleziono miejscowosci {Miejscowosc.nazwa_miejscowosci}.";
            }

            // Jeżeli się spełnią te dwa warunki to dodaj wynik
            var zawodnik = dbContext.Zawodnicy.FirstOrDefault(o => o.imie_zawodnika == Zawodnik.imie_zawodnika && o.nazwisko_zawodnika == Zawodnik.nazwisko_zawodnika);
            Miejscowosci miejscowosc = new Miejscowosci();
            miejscowosc = await dbContext.Miejscowosci.FirstOrDefaultAsync(o => o.nazwa_miejscowosci == Miejscowosc.nazwa_miejscowosci);



            ViewData["Message"] = $"Id miejscowosci to {miejscowosc.idmiejscowosci}.";


            if (zawodnik != null && miejscowosc != null)
            {
                ViewData["Message"] = "Dodawanie wyniku";
                var wyniki = new Wyniki();
                wyniki.idwyniki = Wyniki.idwyniki;
                wyniki.Zawodnik.imie_zawodnika = zawodnik.imie_zawodnika;
                wyniki.Zawodnik.nazwisko_zawodnika = zawodnik.nazwisko_zawodnika;
                wyniki.Zawodnik.kraj_pochodzenia = zawodnik.kraj_pochodzenia;
                wyniki.Miejscowosci.nazwa_miejscowosci = miejscowosc.nazwa_miejscowosci;
                wyniki.Miejscowosci.kraj_miejscowosci = miejscowosc.kraj_miejscowosci;
                
                _context.Wyniki.Add(wyniki);
                await _context.SaveChangesAsync();
                ViewData["Message"] = "Pomyślnie dodano wynik dla zawodnika";
            }
        }
    }
}
