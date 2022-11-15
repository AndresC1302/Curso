using Curso.Models;
using Curso.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SimpleLogin.Helper;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Curso.Controllers
{
    public class LoginController : Controller
    {
        CodeStackCTX ctx;
        public LoginController(CodeStackCTX ctx)
        {
            this.ctx = ctx;

        }

        public IActionResult Index()
        {
            return View();
        }
        [BindProperty]
        public UsuarioVM Usuario { get; set; }
        public async Task <IActionResult> Login()
        {
            if (!ModelState.IsValid)
            {
                    return BadRequest(new JObject() { { "StatusCode", 400 }, { "Message", "El usuario ya existe, seleccione otro" } });
                
            }
            else
            {
                var result = await ctx.Usuarios.Where(x => x.Nombre == Usuario.Nombre).SingleOrDefaultAsync();
                if (result == null)
                {
                    return NotFound((new JObject() { { "StatusCode", 404 }, { "Message", "Usuario no encontrado" } }));
                }
                else
                {
                    if (HashHelper.CheckHash(Usuario.Clave, result.Clave, result.Sal))
                    {
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.IdUsuario.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Name, result.Nombre));
                        identity.AddClaim(new Claim(ClaimTypes.Email, "Este es su correo"));
                        var principal=new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { ExpiresUtc=DateTime.Now.AddDays(1),IsPersistent =true});
                        return Ok(result); 
                    }
                    else
                    {
                        var response = new JObject()
                        {
                            {"StatusCode", 403 },
                            {"Message", "Usuario o contraseña no valido"}
                        };
                        return StatusCode(403, response);
                    }
                }
            }
           
        }
        
    }
}
