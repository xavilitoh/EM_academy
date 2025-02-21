using EM.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EM.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Disciplinas> Diciplinas { get; set; }
    public DbSet<Atleta> Atletas { get; set; }
    public DbSet<Persona> Personas { get; set; }
}