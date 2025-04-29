namespace EM.Entidades;

public class ModeloBAse
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public bool Enable { get; set; } = true;
}