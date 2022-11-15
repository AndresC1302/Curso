using System.ComponentModel.DataAnnotations;

namespace Curso.Models.ViewModel
{
    public class UsuarioVM
    {
        [Required(ErrorMessage = "Escriba su usuario.")]

        public string Nombre { get; set; }

        [Required(ErrorMessage = "Escriba su contraseña.")]

        public string Clave { get; set; }
    }
}
