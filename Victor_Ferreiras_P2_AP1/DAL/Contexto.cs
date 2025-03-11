using Microsoft.EntityFrameworkCore;
using Victor_Ferreiras_P2_AP1.Models;
namespace Victor_Ferreiras_P2_AP1.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public virtual DbSet<Ciudades> Ciudades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudades>().HasData(
            new List<Ciudades>()
            {
                new()
                {
                    CiudadId = 1,
                    CiudadNombre = "San Francisco",
                    Monto = 20000,
                },
                new()
                {
                    CiudadId = 2,
                    CiudadNombre = "Santiago",
                    Monto = 650000,
                },
                new()
                {
                    CiudadId = 3,
                    CiudadNombre = "Santo Domingo",
                    Monto = 3500000,
                }
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}