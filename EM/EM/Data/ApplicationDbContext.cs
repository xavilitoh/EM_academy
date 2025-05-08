using EM.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EM.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
        
    }
    
    public DbSet<Disciplinas> Diciplinas { get; set; }
    public DbSet<Atleta> Atletas { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<TipoUtileria> TiposUtileria { get; set; }
    public DbSet<HistorialMedico> HistorialesMedicos { get; set; }
    public new DbSet<ApplicationUser>  Users { get; set; }
    public DbSet<Utileria> Utilerias { get; set; }
    public DbSet<FacturasAtletas> FacturasAtletas { get; set; }
    public DbSet<PagosFacturas> PagosFacturas { get; set; }
    public DbSet<ContactoAtleta> ContactoAtletas { get; set; }
}