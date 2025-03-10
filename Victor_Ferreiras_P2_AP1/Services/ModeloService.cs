namespace Victor_Ferreiras_P2_AP1.Services;
using Victor_Ferreiras_P2_AP1.Models;
using Victor_Ferreiras_P2_AP1.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class ModeloService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Modelo modelo)
    {   
        return true;
    }

    public async Task<bool> Existe(int ModeloId)
    {
        return true;
    }

    public async Task<bool> Insertar(Modelo modelo)
    {
        return true;
    }
    
    public async Task<bool> Modificar(Modelo modelo)
    {
        return true;
    }
    
    public async Task<Modelo?> Buscar(int ModeloId)
    {
        return new Modelo();
    }
    
    public async Task<List<Modelo>> Listar(Expression<Func<Modelo, bool>> criterio)
    {
        return new List<Modelo>();
    }
}