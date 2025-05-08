using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IContactosAtletasRepositorio : IRepositorioBase<ContactoAtleta>
{
    
}

public class ContactosAtletasRepositorio : IContactosAtletasRepositorio
{
    private readonly ApplicationDbContext _dbContext;

    public ContactosAtletasRepositorio(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ContactoAtleta> Save(ContactoAtleta modelo)
    {
        try
        {
            var result = await _dbContext.ContactoAtletas.AddAsync(modelo);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al guardar el contacto del atleta");
        }
    }

    public async Task<int> Update(ContactoAtleta modelo)
    {
        try
        {
            var toUpdate = await _dbContext.ContactoAtletas
                .FirstOrDefaultAsync(a => a.Id == modelo.Id);

            if (toUpdate != null)
            {
                _dbContext.Entry(toUpdate).CurrentValues.SetValues(modelo);
            }

            if (toUpdate != null) _dbContext.ContactoAtletas.Update(toUpdate);
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al actualizar el contacto del atleta");
        }
    }

    public async Task<ContactoAtleta?> Get(int id)
    {
        try
        {
            return await _dbContext
                .ContactoAtletas
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener el contacto del atleta");
        }
    }

    public async Task<List<ContactoAtleta>> Get()
    {
        try
        {
            return await _dbContext
                .ContactoAtletas
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al obtener los contactos de los atletas");
        }
    }

    public Task<List<SelectListItem>> SelectList()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CambiaEstado(int id, bool nuevoEstado = false)
    {
        throw new NotImplementedException();
    }
}