using System.ComponentModel.DataAnnotations;
namespace Victor_Ferreiras_P2_AP1.Models;

public class Ciudades
{
    [Key]
    public int CiudadId { get; set; }
    [Required(ErrorMessage = "Este campo es requerido")]
    public string CiudadNombre { get; set; } = null!;
    [Range(0.0000000, Double.MaxValue, ErrorMessage = "El monto debe ser positivo")]
    [Required(ErrorMessage = "Este campo es requerido")]
    public double Monto { get; set; }
}