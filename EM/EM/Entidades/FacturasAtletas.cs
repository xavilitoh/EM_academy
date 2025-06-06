using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EM.Entidades;

public class FacturasAtletas : ModeloBAse
{
    public int IdAtleta { get; set; }
    
    [Precision(18,2)]
    public decimal Monto { get; set; }
    
    [Precision(18,2)]
    public decimal Resto { get; set; }

    public bool Pagada { get; set; } = false;
    
    [ForeignKey("IdAtleta")]
    public virtual Atleta Atleta{ get; set; }
}