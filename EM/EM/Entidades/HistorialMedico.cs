using System.ComponentModel.DataAnnotations.Schema;

namespace EM.Entidades;

public class HistorialMedico : ModeloBAse
{
    public int IdAtleta { get; set; }
    public string Diagnostico { get; set; } = string.Empty;
    public string Tratamiento { get; set; } = string.Empty;
    public DateTime FechaConsulta { get; set; } = DateTime.Now;

    [ForeignKey("IdAtleta")] public Atleta? Atleta { get; set; }
}