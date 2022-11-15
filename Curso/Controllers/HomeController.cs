using Curso.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SimpleLogin.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        CodeStackCTX ctx;
        public HomeController(CodeStackCTX _ctx)
        {
            ctx = _ctx;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Results()
        {
            return View();
        }
        public IActionResult Aspirants()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }

        [BindProperty]
        public Usuarios Usuario { get; set; }
        [HttpPost]
        public async Task<IActionResult> Registrar()
        {
            var result = await ctx.Usuarios.Where(x => x.Nombre == Usuario.Nombre).SingleOrDefaultAsync();
            if(result != null)
            {
                return BadRequest(new JObject() {{"StatusCode", 400 },{ "Message", "El usuario ya existe, seleccione otro" } });
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.SelectMany(x=>x.Value.Errors.Select(y=>y.ErrorMessage)).ToList());
                }
                else
                {
                    var hash = HashHelper.Hash(Usuario.Clave);
                    Usuario.Clave = hash.Password;
                    Usuario.Sal= hash.Salt;
                    ctx.Usuarios.Add(Usuario);
                    await ctx.SaveChangesAsync();
                    Usuario.Clave = "";
                    Usuario.Sal = "";
                    return Created($"/Usuarios/{Usuario.IdUsuario}", Usuario);
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
