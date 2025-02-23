using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface ITipoUtileriaRepositorio : IRepositorioBase<TipoUtileria>
{
}

public class TipoUtileriaRepositorio : ITipoUtileriaRepositorio
{
    private readonly ApplicationDbContext _dbContext;

    public TipoUtileriaRepositorio(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
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
        return false;
    }
}