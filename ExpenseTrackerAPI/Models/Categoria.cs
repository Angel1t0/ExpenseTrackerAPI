namespace ExpenseTrackerAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Gasto> Gastos { get; set; }
    }
}
