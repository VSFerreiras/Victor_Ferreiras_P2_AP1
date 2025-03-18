namespace Victor_Ferreiras_P2_AP1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EncuestasDetalle
{
    [Key] public int DetalleId { get; set; }
    public int CiudadId { get; set; }

    [Range(0.0000000, double.MaxValue, ErrorMessage = "El monto debe ser positivo")]
    [Required(ErrorMessage = "Este campo es requerido")]
    public double Monto { get; set; }

    [ForeignKey("CiudadId")] public Ciudades Ciudad { get; set; } = null!;
    public int EncuestaId { get; set; }

    [ForeignKey(nameof(EncuestaId))] public Encuestas Encuestas { get; set; } = null!;
}