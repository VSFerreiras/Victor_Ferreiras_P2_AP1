namespace Victor_Ferreiras_P2_AP1.Models;
using System.ComponentModel.DataAnnotations;
public class Encuestas
{
    [Key]
    public int EncuestaId { get; set; }
    public DateTime FechaEncuesta { get; set; }
    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(maximumLength: 60, ErrorMessage = "La asignatura no debe exceder los 60 caracteres")]
    public string Asignatura { get; set; }
    public List<EncuestasDetalle> EncuestasDetalles { get; set; } = new List<EncuestasDetalle>();
    public double MontoTotal { get; set; }
}