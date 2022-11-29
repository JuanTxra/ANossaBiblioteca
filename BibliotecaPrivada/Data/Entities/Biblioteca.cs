using Microsoft.AspNetCore.Identity;

// Sempre que se cria uma conta nova, cria-se uma biblioteca para esse utilizador
namespace BibliotecaPrivada.Data.Entities
{
    public class Biblioteca
    {
        public int Id { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
    }
}

