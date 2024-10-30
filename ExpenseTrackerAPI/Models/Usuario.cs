using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        // Campo requerido, con un mínimo de 6 caracteres (Mayúscula, número, caracter)
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$")]
        public string Password { get; set; }
        public string FechaCreacion { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        public virtual ICollection<Gasto> Gastos { get; set; }
    }
}
