using EM.Data;
using EM.Entidades;
using EM.Extentions;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IAtletaRepositorio : IRepositorioBase<Atleta>
{
    int Cantidad();
}

public class AtletaRepositorio(ApplicationDbContext context) : IAtletaRepositorio
{
    public async Task<Atleta> Save(Atleta modelo)
    {
        try
        {
            if (modelo.Persona.Edad  < 4)
            {
                throw new Exception("El atleta no puede ser menor de 4 años");
            }
            var persona = await context.Personas.AddAsync(modelo.Persona);
            modelo.IdPersona = persona.Entity.Id;
            modelo.Persona = persona.Entity;
            await context.Atletas.AddAsync(modelo);
            await context.SaveChangesAsync();
            return modelo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.GetMEssage());
        }
    }

    public async Task<int> Update(Atleta modelo)
    {
        try
        {
            var toUpdate = await context.Atletas
                .Include(a => a.Persona)
                .FirstOrDefaultAsync(a => a.Id == modelo.Id);

            if (toUpdate != null)
            {
                context.Entry(toUpdate).CurrentValues.SetValues(modelo);
                context.Entry(toUpdate.Persona).CurrentValues.SetValues(modelo.Persona);
            }

            context.Atletas.Update(toUpdate);
            return await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.GetMEssage());
        }
    }

    public async Task<Atleta?> Get(int id)
    {
        return await context
            .Atletas
            .Include(a => a.Persona)
            .Include(a => a.Disciplinas)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Atleta>> Get()
    {
        return await context
            .Atletas
            .Include(a => a.Persona)
            .Include(a => a.Disciplinas)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<SelectListItem>> SelectList()
    {
        try
        {
            return await context
                .Atletas
                .Include(a => a.Persona)
                .AsNoTracking()
                .Select(d => new SelectListItem
                {
                    Id = d.Id,
                    Value = d.Persona.FullName
                })
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.GetMEssage());
        }
    }

    public async Task<bool> CambiaEstado(int id, bool nuevoEstado = false)
    {
        return false;
    }

    public int Cantidad()
    {
        return context.Atletas
            .AsNoTracking()
            .Count();
    }
}