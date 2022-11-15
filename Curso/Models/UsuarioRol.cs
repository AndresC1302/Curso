using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.Models
{
    [Table("UsuarioRol")]
    public class UsuarioRol
    {
        [Key]   
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public virtual Roles Rol { get; set; }
    }
}
