using System.ComponentModel.DataAnnotations;

namespace EM.Entidades;

public class Persona
{
    public int Id { get; set; }
    
    [MinLength(3, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    public string Nombre { get; set; } = string.Empty;
    
    [MinLength(3, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    public string Apellido { get; set; } = string.Empty;
    
    [MinLength(3, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    public string Nacionalidad { get; set; } = "DOMINICANA";
    
    [MinLength(1, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(1, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    [RegularExpression("^(M|F)$", ErrorMessage = "El campo {0} solo acepta los valores 'M' o 'F'")]
    public string Genero { get; set; } = string.Empty;

    [MinLength(3, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(250, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    public string Direccion { get; set; } = string.Empty;

    [MinLength(12, ErrorMessage = "El campo {0} debe ser mayor a {1} caracteres")]
    [MaxLength(12, ErrorMessage = "El campo {0} debe ser menor a {1} caracteres")]
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "El campo {0} debe tener el formato 000-000-0000")]
    public string Telefono { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public string FullName
    {
        get
        {
            return $"{Nombre} {Apellido}";
        }
    }

    public int Edad 
        { 
            get 
            {
                var today = DateTime.Today;
                var age = today.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
}