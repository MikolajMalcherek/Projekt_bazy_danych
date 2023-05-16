using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using bazy.Data;
using bazy.Models;

namespace bazy.Pages
{
    public class WyszukajPoImieniuModel : PageModel
    {
        private readonly bazy.Data.ZawodnikDbContext _context;

        public WyszukajPoImieniuModel(bazy.Data.ZawodnikDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public string Name { get; set; }

        public IList<Zawodnik> Zawodnicy;

        public async Task OnGetAsync()
        {
            Zawodnicy = await _context.Zawodnicy.ToListAsync();
        }

        public async Task OnPostAsync()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Zawodnicy = await _context.Zawodnicy.ToListAsync();
            }
            else
            {
                Zawodnicy = await _context.Zawodnicy.Where(o => o.imie_zawodnika == Name).ToListAsync();
            }

            if (Zawodnicy.Count == 0)
            {
                ViewData["Message"] = $"Nie znaleziono osoby o imieniu {Name}.";
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var zawodnik = await _context.Zawodnicy.FindAsync(id);

            if (zawodnik != null)
            {
                _context.Zawodnicy.Remove(zawodnik);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

    }
}
