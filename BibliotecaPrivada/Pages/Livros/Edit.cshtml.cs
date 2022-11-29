using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaPrivada.Data;
using BibliotecaPrivada.Data.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaPrivada.Pages.Livros
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly BibliotecaPrivada.Data.ApplicationDbContext _context;

        public EditModel(BibliotecaPrivada.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Livro Livro { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }
            var biblioteca = await _context.Bibliotecas.Where(r => r.UserId == userId).FirstOrDefaultAsync();
            var livro =  await _context.Livros.Where(r => r.BibliotecaId == biblioteca!.Id).FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }
            Livro = livro;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(Livro.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LivroExists(int id)
        {
          return _context.Livros.Any(e => e.Id == id);
        }
    }
}
