using BibliotecaPrivada.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// 1. Alteração do modelo
// 2. Criar a migração add-migration <nome-migração>
// 3. Correr migrações update-database

namespace BibliotecaPrivada.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Biblioteca> Bibliotecas { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}