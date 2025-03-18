namespace Victor_Ferreiras_P2_AP1.Services;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DAL;
using Models;
public class CiudadesService (IDbContextFactory<Contexto> DbFactory)
{
    public async Task<Ciudades?> Buscar(int CiudadId)
    {
        await using var Contexto = await DbFactory.CreateDbContextAsync();
        return await Contexto.Ciudades.FirstOrDefaultAsync(c => c.CiudadId == CiudadId);
    }
    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> Criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades.Where(Criterio).AsNoTracking().ToListAsync();
    }
}