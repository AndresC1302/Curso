using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Curso.Models
{
    public class CodeStackCTX:DbContext
    {
        public CodeStackCTX(DbContextOptions<CodeStackCTX>options):base(options)
        {
            
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }

    }
}
