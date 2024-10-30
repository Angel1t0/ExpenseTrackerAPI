using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Models
{
    public class Gasto
    {
        [Key]
        public  int Id { get; set; }
        public int UsuarioId { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        public int FechaGasto { get; set; }
        [Required]
        [Range(0, 9999999)]
        public float Monto { get; set; }
        public string FechaCreacion { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }
    }
}
