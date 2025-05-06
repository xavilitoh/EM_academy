using EM.Data;
using EM.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EM.Workers;

public class FacturasWorker : IHostedService, IDisposable
{
    private readonly ILogger<FacturasWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    public FacturasWorker(ILogger<FacturasWorker> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Facturas Worker running.");

        // var now = DateTime.Now;
        // var nextRun = now.Date.AddDays(now.Hour >= 8 ? 1 : 0).AddHours(8);
        // var initialDelay = nextRun - now;
        //
        // _timer = new Timer(DoWork, null, initialDelay, TimeSpan.FromDays(1));
        
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        
        _logger.LogInformation("Validando facturas...");
        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var atletas = dbContext.Atletas
                .Include(a=> a.Disciplinas)
                .Include(a=> a.Persona)
                .ToList();
            
            var fechaActual = DateTime.Now;
            
            foreach (var atleta in atletas)
            {
                var facturaExistente = dbContext.FacturasAtletas
                    .FirstOrDefault(f => f.IdAtleta == atleta.Id && 
                                         f.FechaRegistro.Month == fechaActual.Month && 
                                         f.FechaRegistro.Year == fechaActual.Year);
            
                if (facturaExistente == null)
                {
                    var nuevaFactura = new FacturasAtletas
                    {
                        IdAtleta = atleta.Id,
                        Monto = atleta.Disciplinas?.MontoMensualidad ?? 0,
                        Pagada = false,
                        FechaRegistro = fechaActual
                    };
            
                    dbContext.FacturasAtletas.Add(nuevaFactura);
                    _logger.LogInformation("Factura creada para el atleta {Atleta} con monto {Monto}", atleta.Persona.FullName, nuevaFactura.Monto);
                }
            }
            
            dbContext.SaveChanges();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Facturas Worker stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}