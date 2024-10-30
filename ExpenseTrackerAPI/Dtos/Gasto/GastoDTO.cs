using ExpenseTrackerAPI.Dtos.Categoria;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.Gasto
{
    public class GastoDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaGasto { get; set; }
        public float Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public CategoriaDTO Categoria { get; set; }
    }
}
