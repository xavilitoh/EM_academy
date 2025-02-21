namespace EM.Entidades;

public class TipoUtileria
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}