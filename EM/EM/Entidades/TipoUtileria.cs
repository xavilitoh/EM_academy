namespace EM.Entidades;

public class TipoUtileria : ModeloBAse
{
    public virtual ICollection<Utileria> Utilerias { get; set; }
}