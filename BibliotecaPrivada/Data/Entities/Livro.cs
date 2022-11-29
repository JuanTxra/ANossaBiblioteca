using Microsoft.EntityFrameworkCore;

namespace BibliotecaPrivada.Data.Entities
{
    public class Livro
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BibliotecaId { get; set; }

    }

    public class BookFilter
    {
        public string Name { get; set; }
        //public int? AnoEdicao { get; set; }
        //public string UserId { get; set; }


        public BookFilter()
        {
            //AnoEdicao = null;
            Name = null;
            //UserId = null;
        }
    }

    public class BookRepository
    {
        private readonly ApplicationDbContext _ctx;

        public BookRepository(ApplicationDbContext ctx)
        {
            this._ctx = ctx;
        }

        public async Task<List<Livro>> FindByFilterAsync(BookFilter filter = null)
        {
            var livros = _ctx.Livros.AsQueryable();

            if (filter == null)
                return await livros.ToListAsync();

            if (filter.Name != null)
                livros = livros.Where(liv => liv.Name.Contains(filter.Name));

            //if (filter.AnoEdicao != null)
            //    livros = livros.Where(liv => liv.AnoEdicao == filter.AnoEdicao);

            //if (filter.UserId != null)
            //    livros = livros.Where(liv => liv.UserId == filter.UserId);

            return await livros.ToListAsync();
        }
    }
}
