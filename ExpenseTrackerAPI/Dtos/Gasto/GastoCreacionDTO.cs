using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.Gasto
{
    public class GastoCreacionDTO
    {
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        public int FechaGasto { get; set; }
        [Required]
        [Range(0, 9999999)]
        public float Monto { get; set; }
    }
}
