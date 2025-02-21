using System.ComponentModel.DataAnnotations;

namespace EM.Entidades;

public class Disciplinas
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}