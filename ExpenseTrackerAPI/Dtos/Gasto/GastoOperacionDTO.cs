using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.Gasto
{
    public class GastoOperacionDTO
    {
        [Required]
        public string Descripcion { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoriaId { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "La fecha no tiene un formato válido.")]
        public DateTime FechaGasto { get; set; }
        [Required]
        [Range(0, 9999999)]
        public float Monto { get; set; }
    }
}
