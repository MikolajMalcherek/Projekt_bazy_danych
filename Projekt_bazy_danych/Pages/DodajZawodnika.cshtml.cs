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
        public Zawodnik Zawodnik { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Zawodnicy == null || Zawodnik == null)
            {
                return Page();
            }


          if (_context.Zawodnicy.Where(o => o.imie_zawodnika == Zawodnik.imie_zawodnika && o.nazwisko_zawodnika == Zawodnik.nazwisko_zawodnika).Count() > 0)
            {
                ModelState.AddModelError("", $"Zawodnik {Zawodnik.imie_zawodnika} {Zawodnik.nazwisko_zawodnika} już jest w bazie danych!");
                return Page();
            }

            //var query = "SELECT COUNT(*) FROM zawodnicy WHERE imie_zawodnika = @imie_zawodnika AND nazwisko_zawodnika = @nazwisko_zawodnika; SELECT LAST_INSERT_ID();";
            //var parameters = new List<MySqlParameter>
            //{
            //    new MySqlParameter("@imie_zawodnika", Zawodnik.imie_zawodnika),
            //    new MySqlParameter("@nazwisko_zawodnika", Zawodnik.nazwisko_zawodnika)
            //};

            //var result = await _context.Zawodnicy.FromSqlRaw(query, parameters.ToArray()).FirstOrDefaultAsync();



            //Argument metody "FromSqlRaw" to tablica parametrów, które są przekazywane do zapytania SQL jako wartości parametrów. W przykładzie zapytanie ma dwa parametry o nazwach "@Imie" i "@Nazwisko", które są przypisywane do wartości w tablicy "parameters" za pomocą metody "AddWithValue".
            //Metoda "FirstOrDefaultAsync" zwraca pierwszy element z wyniku zapytania lub null, jeśli wynik jest pusty. Dzięki użyciu metody asynchronicznej(Async) wykonanie zapytania nie blokuje wątku i umożliwia płynne działanie aplikacji.



            _context.Zawodnicy.Add(Zawodnik);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
