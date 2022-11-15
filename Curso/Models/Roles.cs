using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.Models
{
    [Table("Roles")]
    public partial class Roles
    {
                [Key, DatabaseGenerated(DatabaseGeneratedOption.None),Column("Id")]
                public int IdRol {get;set;}
                public string Descripcion { get;set;}

    }
}
