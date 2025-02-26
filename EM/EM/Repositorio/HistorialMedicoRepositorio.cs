using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IHistorialMedicoRepositorio : IRepositorioBase<HistorialMedico>
{
    Task<List<HistorialMedico>> GetByAtletaId(int AtletaId);
}

public class HistorialMedicoRepositorio : IHistorialMedicoRepositorio
{
    private readonly ApplicationDbContext _dbContext;

    public HistorialMedicoRepositorio(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HistorialMedico> Save(HistorialMedico modelo)
    {
        try
        {
            var result = await _dbContext.HistorialesMedicos.AddAsync(modelo);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al guardar el Historial Médico");
        }
    }

    public async Task<int> Update(HistorialMedico modelo)
    {
        try
        {
            var toUpdate = await _dbContext.HistorialesMedicos
                .FirstOrDefaultAsync(a => a.Id == modelo.Id);

            if (toUpdate != null)
            {
                _dbContext.Entry(toUpdate).CurrentValues.SetValues(modelo);
            }

            if (toUpdate != null) _dbContext.HistorialesMedicos.Update(toUpdate);
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al actualizar el Historial Médico");
        }
    }

    public async Task<HistorialMedico?> Get(int id)
    {
        try
        {
            return await _dbContext
                .HistorialesMedicos
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener el Historial Médico");
        }
    }

    public async Task<List<HistorialMedico>> Get()
    {
        try
        {
            return await _dbContext
                .HistorialesMedicos
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener los Historiales Médicos");
        }
    }

    public async Task<List<SelectListItem>> SelectList()
    {
        try
        {
            return await _dbContext
                .HistorialesMedicos
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

    public async Task<List<HistorialMedico>> GetByAtletaId(int AtletaId)
    {
        try
        {
            return await _dbContext
                .HistorialesMedicos
                .AsNoTracking()
                .Where(a => a.IdAtleta == AtletaId)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener los Historiales Médicos");
        }
    }
}