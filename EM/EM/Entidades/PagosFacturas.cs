using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EM.Entidades;

public class PagosFacturas : ModeloBAse
{
    public int IdFactura { get; set; }
    public int IdAtleta { get; set; }
    
    [Precision(18,2)]
    public decimal Monto { get; set; }
    
    [ForeignKey("IdAtleta")]
    public virtual Atleta Atleta{ get; set; }
    
    [ForeignKey("IdFactura")]
    public virtual FacturasAtletas Factura{ get; set; }
}