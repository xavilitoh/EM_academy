using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EM.Data;

namespace EM.Entidades;

public class MovimientoUtileria
{
    [Key] public int Id { get; set; }
    public Guid Code { get; set; } = Guid.NewGuid();
    public DateTime FechaMovimiento { get; set; } = DateTime.Now;
    public int IdUtileria { get; set; }
    public string IdUsuario { get; set; } = string.Empty;
    public decimal Cantidad { get; set; }
    public TipoMovimiento TipoMovimiento { get; set; }
    public string Observacion { get; set; } = string.Empty;

    [ForeignKey("IdUtileria")] public virtual Utileria? Utileria { get; set; }
    [ForeignKey("IdUsuario")] public virtual ApplicationUser? Usuario { get; set; }
}

public enum TipoMovimiento
{
    Ingreso,
    Retiro
}
