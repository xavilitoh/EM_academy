using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EM.Entidades;

public class Utileria
{
    [Key] public int Id { get; set; }
    public Guid Code { get; set; } = new Guid();
    public DateTime FechaRegistro { get; set; }
    public int IdTipo { get; set; }
    public int IdMarca { get; set; }

    [ForeignKey("IdTipo")] public TipoUtileria Tipo { get; set; }

    [ForeignKey("IdMarca")] public Marca Mabrca { get; set; }
}