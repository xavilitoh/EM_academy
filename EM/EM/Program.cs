using EM.Components;
using EM.Components.Account;
using EM.Data;
using EM.Entidades;
using EM.Repositorio;
using EM.Workers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HostOptions>(options =>
{
    options.ServicesStartConcurrently = true;
    options.ServicesStopConcurrently = false;
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"))
    ? "DataSource=/app/data/app.db;Cache=Shared"
    : builder.Configuration.GetConnectionString("DefaultConnection") ??
      throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

Console.WriteLine(connectionString);
builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlite(connectionString); }
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<IDiciplinaRepositorio, DiciplinaRepositorio>();
builder.Services.AddScoped<IAtletaRepositorio, AtletaRepositorio>();
builder.Services.AddScoped<IMarcaRepositorio, MarcaRepositorio>();
builder.Services.AddScoped<ITipoUtileriaRepositorio, TipoUtileriaRepositorio>();
builder.Services.AddScoped<IHistorialMedicoRepositorio, HistorialMedicoRepositorio>();
builder.Services.AddScoped<IUtileriaRepositorio, UtileriaRepositorio>();
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ThemeService>();

builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "theme"; // The name of the cookie
    options.Duration = TimeSpan.FromDays(365); // The duration of the cookie
});


builder.Services.AddHostedService<FacturasWorker>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        await context.Database.MigrateAsync();
        await SeedDefaultUser(services);
        await SeedDefaultDisciplinas(services);
        await SeedDefaultMarcas(services);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
app.Run();


static async Task SeedDefaultUser(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var user = await userManager.FindByEmailAsync("usuario1@prueba.dev");
    if (user == null)
    {
        user = new ApplicationUser
        {
            Nombre = "Usuario",
            Apellido = "Prueba",
            UserName = "usuario1@prueba.dev",
            Email = "usuario1@prueba.dev",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, "Abcd1234.");
        
        ApplicationUser user2 = new ApplicationUser
        {
            Nombre = "Usuario2",
            Apellido = "Prueba",
            UserName = "usuario2@prueba.dev",
            Email = "usuario2@prueba.dev",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user2, "Abcd1234.");
    }
}

static async Task SeedDefaultMarcas(IServiceProvider serviceProvider)
{
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Marcas.Any())
    {
        var marcas = new List<Marca>
        {
            new Marca { Descripcion = "Wilson" },
            new Marca { Descripcion = "Under Armour" },
            new Marca { Descripcion = "STAR" },
            new Marca { Descripcion = "Rawlings" },
            new Marca { Descripcion = "Molten" },
            new Marca { Descripcion = "Mikasa" },
            new Marca { Descripcion = "Adidas" }
        };

        foreach (var marca in marcas)
        {
            if (!context.Marcas.Any(d => d.Descripcion == marca.Descripcion))
            {
                context.Marcas.Add(marca);
            }
        }

        await context.SaveChangesAsync();
    }
}


static async Task SeedDefaultDisciplinas(IServiceProvider serviceProvider)
{
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Diciplinas.Any())
    {
        var disciplinas = new List<Disciplinas>
        {
            new Disciplinas { Descripcion = "Base Ball", MontoMensualidad = 2000},
            new Disciplinas { Descripcion = "Basket Ball", MontoMensualidad = 2200 },
            new Disciplinas { Descripcion = "Foot Ball", MontoMensualidad = 3000 }
        };

        foreach (var disciplina in disciplinas)
        {
            if (!context.Diciplinas.Any(d => d.Descripcion == disciplina.Descripcion))
            {
                context.Diciplinas.Add(disciplina);
            }
        }

        await context.SaveChangesAsync();
    }
}