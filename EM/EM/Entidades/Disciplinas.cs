namespace EM.Entidades;

public class Disciplinas : ModeloBAse
{
    public virtual ICollection<Atleta> Atletas { get; set; }
}