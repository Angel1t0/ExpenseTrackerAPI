using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.Usuario
{
    public class UsuarioRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string FechaCreacion { get; set; }
    }
}
