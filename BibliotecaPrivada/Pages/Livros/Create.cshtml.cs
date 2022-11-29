using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BibliotecaPrivada.Data;
using BibliotecaPrivada.Data.Entities;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaPrivada.Pages.Livros
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly BibliotecaPrivada.Data.ApplicationDbContext _context;

        public CreateModel(BibliotecaPrivada.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Livro Livro { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bibliotecaId = _context.Bibliotecas.Where(r => r.UserId == userId).FirstOrDefault();
            Livro.BibliotecaId = bibliotecaId!.Id;
            _context.Livros.Add(Livro);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
