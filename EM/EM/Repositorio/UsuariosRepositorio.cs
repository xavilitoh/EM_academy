using EM.Data;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IUsuariosRepositorio
{
    Task<ApplicationUser?> ObtenerUsuarioPorId(string id);
    Task<List<ApplicationUser>> ObtenerUsuarios();
    Task<ApplicationUser> Save(ApplicationUser usuario);
    
}

public class UsuariosRepositorio : IUsuariosRepositorio
{
    private readonly ApplicationDbContext _context;

    public UsuariosRepositorio(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser?> ObtenerUsuarioPorId(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<List<ApplicationUser>> ObtenerUsuarios()
    {
        return await _context.Users.ToListAsync();
    }

    public Task<ApplicationUser> Save(ApplicationUser usuario)
    {
        throw new NotImplementedException();
    }
}