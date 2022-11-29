using BibliotecaPrivada.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BibliotecaPrivada.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BibliotecaPrivada.Data.ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, BibliotecaPrivada.Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Livro> Livro { get; set; } = default!;
        public async Task OnGetAsync()
        {

            if (_context.Livros != null)
            {
                Livro = await _context.Livros.ToListAsync();
            }
            
        }

        // Maldição para quem está a ler isto

        public String nameFilter { get; set; }

        public async Task OnPostAsync(BookRepository repositorio)
        {
            var filter = new BookFilter();
            filter.Name = nameFilter;


            Livro = await repositorio.FindByFilterAsync(filter);

        }


    }
}