using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.Usuario
{
    public class UsuarioLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Campo requerido, con un mínimo de 6 caracteres (Mayúscula, número, caracter)
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$")]
        public string Password { get; set; }
    }
}
