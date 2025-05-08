using System.ComponentModel.DataAnnotations;

namespace EM.Entidades;

public class ContactoAtleta
{
    [Key]
    public int Id { get; set; }
    public int AtletaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public bool Principal { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public bool Activo { get; set; } = true;

    // Relacion
    public virtual Atleta Atleta { get; set; } = null!;
}