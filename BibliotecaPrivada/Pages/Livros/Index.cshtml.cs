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
    public class IndexModel : PageModel
    {
        private readonly BibliotecaPrivada.Data.ApplicationDbContext _context;

        public IndexModel(BibliotecaPrivada.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Livro> Livro { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var biblioteca = _context.Bibliotecas.Where(r => r.UserId == userId).FirstOrDefault();

            if (_context.Livros != null)
            {
                Livro = await _context.Livros.Where(r => r.BibliotecaId == biblioteca!.Id).ToListAsync();
            }
        }

        public async Task<List<Livro>> FindByFilterAsync(BookFilter filter = null)
        {
            var livros = _context.Livros.AsQueryable();

            if (filter == null)
                return await livros.ToListAsync();

            if (filter.Name != null)
                livros = livros.Where(liv => liv.Name.Contains(filter.Name));

            return await livros.ToListAsync();
        }
    }
}
