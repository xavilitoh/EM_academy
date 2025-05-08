using System.ComponentModel.DataAnnotations;

namespace EM.Entidades;

public class ContactoAtleta
{
    [Key]
    public int Id { get; set; }
    public int AtletaId { get; set; }
    
    [Required(ErrorMessage = "El nombre es requerido")]
    [MinLength(2, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(100, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    public string Nombre { get; set; } = string.Empty;
    
    [MinLength(12, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(12, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "El campo {0} debe tener el formato 000-000-0000")]
    public string Telefono { get; set; } = string.Empty;
    
    [MinLength(2, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(100, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    [DataType(DataType.EmailAddress)]
    public string Correo { get; set; } = string.Empty;
    

    [MinLength(2, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(100, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    public string Direccion { get; set; } = string.Empty;
    public bool Principal { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public bool Activo { get; set; } = true;

    // Relacion
    public virtual Atleta Atleta { get; set; } = null!;
}