using System.ComponentModel.DataAnnotations.Schema;
using EM.Migrations;

namespace EM.Entidades;

public class Atleta
{
    public int Id { get; set; }
    public int IdPersona { get; set; }
    public int IdDisciplina { get; set; }
    public decimal Peso { get; set; }
    public decimal Altura { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    
    [ForeignKey("IdPersona")]
    public Persona Persona { get; set; }
    
    [ForeignKey("IdDisciplina")]
    public Disciplinas Disciplinas { get; set; }
}