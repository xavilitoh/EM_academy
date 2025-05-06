using Microsoft.EntityFrameworkCore;

namespace EM.Entidades;

public class Disciplinas : ModeloBAse
{
    [Precision(18,2)]
    public decimal MontoMensualidad { get; set; }
    public virtual ICollection<Atleta> Atletas { get; set; }
}