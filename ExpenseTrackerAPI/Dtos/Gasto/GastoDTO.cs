using ExpenseTrackerAPI.Dtos.Categoria;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Dtos.Gasto
{
    public class GastoDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int FechaGasto { get; set; }
        public float Monto { get; set; }
        public string FechaCreacion { get; set; }
        public CategoriaDTO Categoria { get; set; }
    }
}
