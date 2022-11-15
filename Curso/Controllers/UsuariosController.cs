using Curso.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Curso.Controllers
{
    public class UsuariosController : Controller
    {
        readonly CodeStackCTX ctx;
        public UsuariosController(CodeStackCTX ctx)
        {
            this.ctx = ctx;
        }

        public async Task<IActionResult> Index()
        {
            return Ok(await ctx.Usuarios.Include("Roles.Rol").ToListAsync());
        }
    }
}
