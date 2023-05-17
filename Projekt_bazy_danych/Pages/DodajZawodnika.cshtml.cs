using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using bazy.Data;
using bazy.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MessagePack;
using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace bazy.Pages
{
    public class DodajZawodnikaModel : PageModel
    {
        private readonly bazy.Data.ZawodnikDbContext _context;

        public DodajZawodnikaModel(bazy.Data.ZawodnikDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Zawodnik Zawodnik { get; set; }

        

        public async Task OnPostAsync()
        {
            ZawodnikDbContext dbContext = new ZawodnikDbContext();

            Console.WriteLine("Zawodnik: " + Zawodnik.imie_zawodnika + Zawodnik.nazwisko_zawodnika + Zawodnik.kraj_pochodzenia);
          if ( _context.Zawodnicy == null || Zawodnik == null)
            {
                ModelState.AddModelError("", $"sdgdsgsdg");
                return;
            }


          if (_context.Zawodnicy.Where(o => o.imie_zawodnika == Zawodnik.imie_zawodnika && o.nazwisko_zawodnika == Zawodnik.nazwisko_zawodnika).Count() > 0)
            {
                ModelState.AddModelError("", $"Zawodnik {Zawodnik.imie_zawodnika} {Zawodnik.nazwisko_zawodnika} już jest w bazie danych!");
                return;
            }

            _context.Zawodnicy.Add(Zawodnik);
            await _context.SaveChangesAsync();
            ModelState.AddModelError("", $"Pomyślnie dodano zawodnika");
            return;
        }
    }
}
 