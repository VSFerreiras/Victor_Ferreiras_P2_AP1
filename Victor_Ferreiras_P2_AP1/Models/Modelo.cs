using System.ComponentModel.DataAnnotations;
namespace Victor_Ferreiras_P2_AP1.Models;

public class Modelo
{
    [Key]
    public int ModeloId { get; set; }
    [Required(ErrorMessage = "El campo es requerido")]
    public string Nombre { get; set; }
}