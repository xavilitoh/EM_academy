using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IContactosAtletasRepositorio : IRepositorioBase<ContactoAtleta>
{
    
}

public class ContactosAtletasRepositorio(ApplicationDbContext dbContext) : IContactosAtletasRepositorio
{
    public async Task<ContactoAtleta> Save(ContactoAtleta modelo)
    {
        if (modelo == null || string.IsNullOrEmpty(modelo.Nombre) || string.IsNullOrEmpty(modelo.Telefono)|| string.IsNullOrEmpty(modelo.Direccion))
        {
            throw new ArgumentException("El modelo o sus campos requeridos no pueden ser nulos.");
        }
        
        try
        {
            if (modelo.Principal)
            {
                var otrosContactos = await dbContext.ContactoAtletas
                    .Where(a => a.AtletaId == modelo.AtletaId)
                    .ToListAsync();
    
                foreach (var contacto in otrosContactos)
                {
                    contacto.Principal = false;
                }
            }
    
            var result = await dbContext.ContactoAtletas.AddAsync(modelo);
            await dbContext.SaveChangesAsync();
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
            var toUpdate = await dbContext.ContactoAtletas
                .FirstOrDefaultAsync(a => a.Id == modelo.Id);
            
            if (toUpdate != null)
            {
                // Si el modelo tiene Principal en true, desactivar Principal en otros contactos del mismo atleta
                if (modelo.Principal)
                {
                    var otrosContactos = await dbContext.ContactoAtletas
                        .Where(a => a.AtletaId == modelo.AtletaId && a.Id != modelo.Id)
                        .ToListAsync();
            
                    foreach (var contacto in otrosContactos)
                    {
                        contacto.Principal = false;
                    }
                }
            
                dbContext.Entry(toUpdate).CurrentValues.SetValues(modelo);
                dbContext.ContactoAtletas.Update(toUpdate);
            }
            
            return await dbContext.SaveChangesAsync();
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
            return await dbContext
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
            return await dbContext
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