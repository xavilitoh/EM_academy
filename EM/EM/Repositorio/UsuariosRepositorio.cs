using EM.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EM.Repositorio;

public interface IUsuariosRepositorio
{
    Task<ApplicationUser?> ObtenerUsuarioPorId(string id);
    Task<List<ApplicationUser>> ObtenerUsuarios();
    Task<ApplicationUser> Save(ApplicationUser usuario, string password = "Password12$");
    
}

public class UsuariosRepositorio : IUsuariosRepositorio
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsuariosRepositorio(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> ObtenerUsuarioPorId(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<List<ApplicationUser>> ObtenerUsuarios()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<ApplicationUser> Save(ApplicationUser usuario, string password = "Password12$")
    {
        try
        {
            var resp = await _userManager.CreateAsync(usuario, password );

            if (resp.Succeeded)
            {
                
                return usuario;
            }
            else
            {
                throw new Exception(resp.Errors.First().Description);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}