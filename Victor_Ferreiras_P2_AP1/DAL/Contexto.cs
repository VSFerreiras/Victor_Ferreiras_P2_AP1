using Microsoft.EntityFrameworkCore;
using Victor_Ferreiras_P2_AP1.Models;
namespace Victor_Ferreiras_P2_AP1.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Modelo> Modelos { get; set; }
}