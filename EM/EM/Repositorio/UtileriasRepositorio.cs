using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IUtileriaRepositorio : IRepositorioBase<Utileria>
{
}

public class UtileriaRepositorio : IUtileriaRepositorio
{
    private readonly ApplicationDbContext _dbContext;

    public UtileriaRepositorio(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Utileria> Save(Utileria modelo)
    {
        try
        {
            var result = await _dbContext.Utilerias.AddAsync(modelo);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al guardar la Utilería");
        }
    }

    public async Task<int> Update(Utileria modelo)
    {
        try
        {
            var toUpdate = await _dbContext.Utilerias
                .FirstOrDefaultAsync(a => a.Id == modelo.Id);

            if (toUpdate != null)
            {
                _dbContext.Entry(toUpdate).CurrentValues.SetValues(modelo);
            }

            var result = _dbContext.Utilerias.Update(toUpdate);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al actualizar la Utilería");
        }
    }

    public async Task<Utileria?> Get(int id)
    {
        try
        {
            var result = await _dbContext
                .Utilerias
                .AsNoTracking()
                .Include(a => a.Tipo)
                .Include(a => a.Marca)
                .FirstOrDefaultAsync(a => a.Id == id);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener la Utilería");
        }
    }

    public async Task<List<Utileria>> Get()
    {
        try
        {
            var result = await _dbContext
                .Utilerias
                .AsNoTracking()
                .Include(a => a.Tipo)
                .Include(a => a.Marca)
                .ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener las Utilerías");
        }
    }

    public async Task<List<SelectListItem>> SelectList()
    {
        try
        {
            var result = await _dbContext
                .Utilerias
                .AsNoTracking()
                .Include(a => a.Tipo)
                .Include(a => a.Marca)
                .Select(a => new SelectListItem
                {
                    Id = a.Id,
                    Value = a.Tipo.Descripcion
                })
                .ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener las Utilerías");
        }
    }

    public async Task<bool> CambiaEstado(int id, bool nuevoEstado = false)
    {
        return false;
    }
}