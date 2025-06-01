using EM.Data;
using EM.Entidades;
using EM.Models;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IUtileriaRepositorio : IRepositorioBase<Utileria>
{
    int Cantidad();
    Task<bool> IngresarUtileria(int idUtileria, decimal cantidad, string idUsuario, string observacion);
    Task<bool> RetirarUtileria(int idUtileria, decimal cantidad, string idUsuario, string observacion);
    Task<List<MovimientoUtileria>> ObtenerMovimientos(int idUtileria);
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

    public int Cantidad()
    {
        int cantidad = 0;

        var data = _dbContext.Utilerias.ToList();

        data.ForEach(a => { cantidad += (int)a.Cantidad; });

        return cantidad;
    }

    public async Task<bool> IngresarUtileria(int idUtileria, decimal cantidad, string idUsuario, string observacion)
    {
        try
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            // Obtener la utilería
            var utileria = await _dbContext.Utilerias.FirstOrDefaultAsync(u => u.Id == idUtileria);
            if (utileria == null)
                throw new Exception("La utilería no existe");

            // Actualizar cantidad
            utileria.Cantidad += cantidad;
            _dbContext.Utilerias.Update(utileria);

            // Registrar el movimiento
            var movimiento = new MovimientoUtileria
            {
                IdUtileria = idUtileria,
                IdUsuario = idUsuario,
                Cantidad = cantidad,
                TipoMovimiento = TipoMovimiento.Ingreso,
                Observacion = observacion
            };

            await _dbContext.MovimientosUtilerias.AddAsync(movimiento);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al ingresar la utilería");
        }
    }

    public async Task<bool> RetirarUtileria(int idUtileria, decimal cantidad, string idUsuario, string observacion)
    {
        try
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            // Obtener la utilería
            var utileria = await _dbContext.Utilerias.FirstOrDefaultAsync(u => u.Id == idUtileria);
            if (utileria == null)
                throw new Exception("La utilería no existe");

            // Verificar que haya suficiente cantidad
            if (utileria.Cantidad < cantidad)
                throw new Exception("No hay suficiente cantidad disponible");

            // Actualizar cantidad
            utileria.Cantidad -= cantidad;
            _dbContext.Utilerias.Update(utileria);

            // Registrar el movimiento
            var movimiento = new MovimientoUtileria
            {
                IdUtileria = idUtileria,
                IdUsuario = idUsuario,
                Cantidad = cantidad,
                TipoMovimiento = TipoMovimiento.Retiro,
                Observacion = observacion
            };

            await _dbContext.MovimientosUtilerias.AddAsync(movimiento);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error al retirar la utilería");
        }
    }

    public async Task<List<MovimientoUtileria>> ObtenerMovimientos(int idUtileria)
    {
        try
        {
            // Verificar si la tabla existe o contiene datos
            if (_dbContext.MovimientosUtilerias == null)
            {
                // Si no existe la tabla o no está configurada correctamente, devolver lista vacía
                return new List<MovimientoUtileria>();
            }

            var movimientos = await _dbContext.MovimientosUtilerias
                .AsNoTracking()
                .Where(m => m.IdUtileria == idUtileria)
                .Include(m => m.Usuario)
                .Include(m => m.Utileria)
                .OrderByDescending(m => m.FechaMovimiento)
                .ToListAsync();

            return movimientos;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            // Si ocurre un error, en lugar de lanzar excepción, devolver lista vacía
            // para evitar que la interfaz de usuario se rompa
            return new List<MovimientoUtileria>();
        }
    }
}