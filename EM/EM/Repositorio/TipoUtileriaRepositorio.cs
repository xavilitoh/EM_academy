using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface ITipoUtileriaRepositorio : IRepositorioBase<TipoUtileria>
{
    int Cantidad();
    Task<List<TipoUtileriaCount>> GetTiposUtileriaConCantidad();
}

public class TipoUtileriaRepositorio : ITipoUtileriaRepositorio
{
    private readonly ApplicationDbContext _dbContext;

    public TipoUtileriaRepositorio(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TipoUtileriaCount>> GetTiposUtileriaConCantidad()
    {
        return await _dbContext.TiposUtileria
            .Select(t => new TipoUtileriaCount
            {
                Tipo = t.Descripcion,
                Cantidad = (int)t.Utilerias.Sum(a => a.Cantidad)
            })
            .ToListAsync();
    }

    public async Task<TipoUtileria> Save(TipoUtileria modelo)
    {
        try
        {
            var result = await _dbContext.TiposUtileria.AddAsync(modelo);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al guardar el Tipo de Utileria");
        }
    }

    public async Task<int> Update(TipoUtileria modelo)
    {
        try
        {
            var toUpdate = await _dbContext.TiposUtileria
                .FirstOrDefaultAsync(a => a.Id == modelo.Id);

            if (toUpdate != null)
            {
                _dbContext.Entry(toUpdate).CurrentValues.SetValues(modelo);
            }

            if (toUpdate != null) _dbContext.TiposUtileria.Update(toUpdate);
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al actualizar el Tipo de Utileria");
        }
    }

    public async Task<TipoUtileria?> Get(int id)
    {
        try
        {
            return await _dbContext
                .TiposUtileria
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener el Tipo de Utileria");
        }
    }

    public async Task<List<TipoUtileria>> Get()
    {
        try
        {
            return await _dbContext
                .TiposUtileria
                .AsNoTracking()
                .Where(a => a.Enable)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener los Tipos de Utileria");
        }
    }

    public async Task<List<SelectListItem>> SelectList()
    {
        try
        {
            return await _dbContext
                .TiposUtileria
                .AsNoTracking()
                .Where(a => a.Enable)
                .Select(d => new SelectListItem
                {
                    Id = d.Id,
                    Value = d.Descripcion
                })
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CambiaEstado(int id, bool nuevoEstado = false)
    {
        var tipoUtileria = await _dbContext.TiposUtileria
            .FirstOrDefaultAsync(a => a.Id == id);
        if (tipoUtileria != null)
        {
            tipoUtileria.Enable = nuevoEstado;
            _dbContext.Entry(tipoUtileria).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            throw new Exception("Error al cambiar el estado del Tipo de Utileria");
        }
    }

    public int Cantidad()
    {
        return _dbContext.TiposUtileria.AsNoTracking()
            .Count(a => a.Enable);
    }
}