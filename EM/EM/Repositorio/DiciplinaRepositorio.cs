using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IDiciplinaRepositorio : IRepositorioBase<Disciplinas>
{
    
}


public class DiciplinaRepositorio : IDiciplinaRepositorio
{
    private readonly ApplicationDbContext _dbContext;

    public DiciplinaRepositorio(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Disciplinas> Save(Disciplinas modelo)
    {
        try
        {
            var result = await _dbContext.Diciplinas.AddAsync(modelo);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al guardar la Diciplina");
        }
    }

    public async Task<int> Update(Disciplinas modelo)
    {
        try
        {
            _dbContext.Diciplinas.Update(modelo);
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al actualizar la Diciplina");
        }
    }

    public async Task<Disciplinas?> Get(int id)
    {
        try
        {
            return await _dbContext
                .Diciplinas
                .AsNoTracking() 
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener la Diciplina");
        }
    }

    public async Task<List<Disciplinas>> Get()
    {
        try
        {
            return await _dbContext
                .Diciplinas
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener las Diciplinas");
        }
    }

    public async Task<List<SelectListItem>> SelectList()
    {
        try
        {
            return await _dbContext
                .Diciplinas
                .AsNoTracking()
                .Select(d => new SelectListItem
                {
                    Id = d.Id,
                    Value= d.Descripcion
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
        throw new NotImplementedException();
    }
}