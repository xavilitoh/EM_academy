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
    public string Nacionalidad { get; set; } = "DOMINICANA";
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