using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IDiciplinaRepositorio : IRepositorioBase<Disciplinas>
{
    int Cantidad();
    Task<List<DisciplinaAtletasCount>> GetDisciplinasConCantidadDeAtletas();
}

public class DiciplinaRepositorio : IDiciplinaRepositorio
{
    private readonly ApplicationDbContext _dbContext;

    public DiciplinaRepositorio(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DisciplinaAtletasCount>> GetDisciplinasConCantidadDeAtletas()
    {
        return await _dbContext.Diciplinas
            .Select(d => new DisciplinaAtletasCount
            {
                Disciplina = d.Descripcion,
                CantidadAtletas = d.Atletas.Count
            })
            .ToListAsync();
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
            var toUpdate = await _dbContext.Diciplinas
                .FirstOrDefaultAsync(a => a.Id == modelo.Id);

            if (toUpdate != null)
            {
                _dbContext.Entry(toUpdate).CurrentValues.SetValues(modelo);
            }

            if (toUpdate != null) _dbContext.Diciplinas.Update(toUpdate);
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
        var diciplina = await _dbContext.Diciplinas
            .FirstOrDefaultAsync(a => a.Id == id);
        if (diciplina != null)
        {
            diciplina.Enable = nuevoEstado;
            _dbContext.Entry(diciplina).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            throw new Exception("Error al cambiar el estado de la diciplina");
        }
    }

    public int Cantidad()
    {
        return _dbContext
            .Diciplinas
            .AsNoTracking()
            .Count(a => a.Enable);
    }
}