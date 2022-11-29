using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BibliotecaPrivada.Data;
using BibliotecaPrivada.Data.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaPrivada.Pages.Livros
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly BibliotecaPrivada.Data.ApplicationDbContext _context;

        public DeleteModel(BibliotecaPrivada.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Livro Livro { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }
            var biblioteca = await _context.Bibliotecas.Where(r => r.UserId == userId).FirstOrDefaultAsync();
            var livro = await _context.Livros.Where(r => r.BibliotecaId == biblioteca!.Id).FirstOrDefaultAsync(m => m.Id == id);

            if (livro == null)
            {
                return NotFound();
            }
            else 
            {
                Livro = livro;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }
            var livro = await _context.Livros.FindAsync(id);

            if (livro != null)
            {
                Livro = livro;
                _context.Livros.Remove(Livro);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
