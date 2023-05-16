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


        [BindProperty]
        public Zawodnik Zawodnik { get; set; } = default!;

        [BindProperty]
        public int wynik { get; set; }
        [BindProperty]
        public Miejscowosci Miejscowosc { get; set; }

        public Wyniki Wyniki { get; set; } = new Wyniki();

        public List<Wyniki> WynikiZawodnicy { get; set; }
        public List<Miejscowosci> ListaMiejscowosci { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadData();
            return Page();
        }


        public async Task OnPostAsync()
        {
            await LoadData();

            // Nie ma takiej osoby
            if (_context.Zawodnicy.Where(o => o.imie_zawodnika == Zawodnik.imie_zawodnika && o.nazwisko_zawodnika == Zawodnik.nazwisko_zawodnika).Count() == 0)
            {
                ViewData["Message"] = $"Nie znaleziono osoby o imieniu {Zawodnik.imie_zawodnika} {Zawodnik.nazwisko_zawodnika}.";
                return;
            }

            // Nie ma takiej miejscowosci zawodów
            if (_context.Miejscowosci.Where(o => o.nazwa_miejscowosci == Miejscowosc.nazwa_miejscowosci).Count() == 0)
            {
                ViewData["Message"] = $"Nie znaleziono miejscowosci {Miejscowosc.nazwa_miejscowosci}.";
                return;
            }

            //Sprawdzenie, czy zawodnik ma już wpisany wynik w danej miejscowosci
            //if (_context.Wyniki.Where(o => o.idzawodnicy == Zawodnik.idzawodnicy).Count() != 0 && _context.Wyniki.Where(o => o.idmiejscowosci == Miejscowosc.idmiejscowosci).Count() != 0)
            //{
            //    ViewData["Message"] = $"Nie można dodać wyniku - ten zawodnik ma już wpisane wyniki w tej miejscowosci!";
            //    return;
            //}

            else
            {
                // Jeżeli się spełnią te dwa warunki to dodaj wynik
                var znajdzzawodnika = await _context.Zawodnicy.FirstOrDefaultAsync(o => o.imie_zawodnika == Zawodnik.imie_zawodnika && o.nazwisko_zawodnika == Zawodnik.nazwisko_zawodnika);
                //Miejscowosci miejscowosc = new Miejscowosci();
                var znajdzmiejscowosc = await _context.Miejscowosci.FirstOrDefaultAsync(o => o.nazwa_miejscowosci == Miejscowosc.nazwa_miejscowosci);

                var zawodnik = await _context.Zawodnicy.FindAsync(znajdzzawodnika?.idzawodnicy);
                var miejscowosc = await _context.Miejscowosci.FindAsync(znajdzmiejscowosc?.idmiejscowosci);
               
                Console.WriteLine("Znaleziony zawodnik: {0}", zawodnik?.nazwisko_zawodnika);
                Console.WriteLine("Znaleziona Miejscowosc: {0}", miejscowosc?.nazwa_miejscowosci);
                
                if (zawodnik != null && miejscowosc != null)
                {
                    var nowyWynik = new Wyniki
                    {
                        wynik = wynik,
                        idzawodnicy = zawodnik.idzawodnicy,
                        idmiejscowosci = miejscowosc.idmiejscowosci
                    };
                    Wyniki.Zawodnik = zawodnik;
                    Wyniki.Miejscowosci = miejscowosc;

                    _context.Wyniki.Add(Wyniki);
                    await _context.SaveChangesAsync();
                    
                }
            }
        }

        private async Task LoadData()
        {
            WynikiZawodnicy = await _context.Wyniki
                .Include(w => w.Zawodnik)
                .Select(w => new Wyniki
                {
                    idwyniki = w.idwyniki,
                    wynik = w.wynik,
                    idzawodnicy = w.idzawodnicy,
                    idmiejscowosci = w.idmiejscowosci,
                    Miejscowosci = w.Miejscowosci,
                    Zawodnik = w.Zawodnik
                })
                .ToListAsync();

            ListaMiejscowosci = await _context.Miejscowosci.ToListAsync();
        }
    }
}
