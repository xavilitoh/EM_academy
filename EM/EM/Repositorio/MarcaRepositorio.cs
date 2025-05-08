using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IMarcaRepositorio : IRepositorioBase<Marca>
{
    int Cantidad();
}

public class MarcaRepositorio : IMarcaRepositorio
{
    private readonly ApplicationDbContext _dbContext;

    public MarcaRepositorio(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Marca> Save(Marca modelo)
    {
        try
        {
            var data = await _dbContext.Marcas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Descripcion == modelo.Descripcion);

            if (data?.Descripcion == modelo.Descripcion)
            {
                data.Enable = true;
                await this.Update(data);
                
                return data;
            }
            
            var result = await _dbContext.Marcas.AddAsync(modelo);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al guardar la Marca");
        }
    }

    public async Task<int> Update(Marca modelo)
    {
        try
        {
            var toUpdate = await _dbContext.Marcas
                .FirstOrDefaultAsync(a => a.Id == modelo.Id);

            if (toUpdate != null)
            {
                _dbContext.Entry(toUpdate).CurrentValues.SetValues(modelo);
            }

            var result = _dbContext.Marcas.Update(toUpdate);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al actualizar la Marca");
        }
    }

    public async Task<Marca?> Get(int id)
    {
        try
        {
            var result = await _dbContext
                .Marcas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener la Marca");
        }
    }

    public async Task<List<Marca>> Get()
    {
        try
        {
            var result = await _dbContext
                .Marcas
                .AsNoTracking()
                .Where(a=> a.Enable)
                .ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener las Marcas");
        }
    }

    public async Task<List<SelectListItem>> SelectList()
    {
        try
        {
            var result = await _dbContext
                .Marcas
                .AsNoTracking()
                .Where(a => a.Enable)
                .Select(a => new SelectListItem
                {
                    Id = a.Id,
                    Value = a.Descripcion
                })
                .ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener las Marcas");
        }
    }

    public async Task<bool> CambiaEstado(int id, bool nuevoEstado = false)
    {
        var marca = await _dbContext.Marcas
            .FirstOrDefaultAsync(a => a.Id == id);
        if (marca != null)
        {
            marca.Enable = nuevoEstado;
            _dbContext.Entry(marca).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            throw new Exception("Error al cambiar el estado de la Marca");
        }
    }

    public int Cantidad()
    {
        return _dbContext.Marcas
            .AsNoTracking()
            .Count(a => a.Enable);
    }
}