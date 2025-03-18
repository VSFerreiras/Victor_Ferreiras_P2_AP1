namespace Victor_Ferreiras_P2_AP1.Services;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models;
using DAL;

public class EncuestasService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Encuestas encuesta)
    {
        if (!await Existe(encuesta.EncuestaId))
        {
            return await Insertar(encuesta);
        }
        else
        {
            return await Modificar(encuesta);
        }
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Encuestas.AnyAsync(e => e.EncuestaId == id);
    }

    public async Task<bool> Insertar(Encuestas encuesta)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Encuestas.Add(encuesta);
        foreach (var detalle in encuesta.EncuestasDetalles)
        {
            var ciudad = await contexto.Ciudades.FindAsync(detalle.CiudadId);
            if (ciudad != null)
                ciudad.Monto += detalle.Monto;
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Encuestas encuestaActualizada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var EncuestaExistente = await contexto.Encuestas
            .Include(e => e.EncuestasDetalles)
            .FirstOrDefaultAsync(e => e.EncuestaId == encuestaActualizada.EncuestaId);

        if (EncuestaExistente == null)
            return false;

        foreach (var DetalleNew in encuestaActualizada.EncuestasDetalles)
        {
            var DetalleOG = EncuestaExistente.EncuestasDetalles.FirstOrDefault(d => d.DetalleId == DetalleNew.DetalleId);
            if (DetalleOG != null)
            {
                var ciudad = await contexto.Ciudades.FindAsync(DetalleNew.CiudadId);
                if (ciudad != null)
                    ciudad.Monto += (DetalleNew.Monto - DetalleOG.Monto);

                contexto.Entry(DetalleOG).CurrentValues.SetValues(DetalleNew);
            }
            else
            {
                EncuestaExistente.EncuestasDetalles.Add(DetalleNew);
                var ciudad = await contexto.Ciudades.FindAsync(DetalleNew.CiudadId);
                if (ciudad != null)
                    ciudad.Monto += DetalleNew.Monto;
            }
        }


        foreach (var DetalleOG in EncuestaExistente.EncuestasDetalles.ToList())
        {
            bool existe = encuestaActualizada.EncuestasDetalles.Any(d => d.DetalleId == DetalleOG.DetalleId);
            if (!existe)
            {
                var ciudad = await contexto.Ciudades.FindAsync(DetalleOG.CiudadId);
                if (ciudad != null)
                    ciudad.Monto -= DetalleOG.Monto;
                EncuestaExistente.EncuestasDetalles.Remove(DetalleOG);
            }
        }

        contexto.Entry(EncuestaExistente).CurrentValues.SetValues(encuestaActualizada);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Encuestas?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Encuestas
            .Where(e => e.EncuestaId == id)
            .Include(e => e.EncuestasDetalles)
            .ThenInclude(d => d.Ciudad)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var encuestas = await contexto.Encuestas
            .Include(e => e.EncuestasDetalles)
            .FirstOrDefaultAsync(e => e.EncuestaId == id);

        if (encuestas == null)
            return false;

        foreach (var detalle in encuestas.EncuestasDetalles)
        {
            var ciudad = await contexto.Ciudades.FindAsync(detalle.CiudadId);
            if (ciudad != null)
                ciudad.Monto -= detalle.Monto;
        }

        contexto.Encuestas.Remove(encuestas);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Encuestas>> Listar(Expression<Func<Encuestas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Encuestas
            .Include(e => e.EncuestasDetalles)
            .ThenInclude(d => d.Ciudad)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}