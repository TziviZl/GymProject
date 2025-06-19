using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

public class StudioClassResetService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private bool _hasRunToday = false;

    public StudioClassResetService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;

            if (now.DayOfWeek == DayOfWeek.Saturday && now.Hour >= 20)
            {
                if (!_hasRunToday)
                {
                    await ResetClassesAsync();
                    _hasRunToday = true;
                }
            }
            else
            {
                _hasRunToday = false; 
            }

            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken); 
        }
    }

    public async Task ResetClassesAsync()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DB_Manager>();
            var allClasses = await db.StudioClasses.ToListAsync();

            foreach (var c in allClasses)
            {
                c.Date = c.Date.AddDays(7);
                c.CurrentNum = 20;
            }

            await db.SaveChangesAsync();
            Console.WriteLine("✅ Studio classes reset for the new week.");
        }
    }

}
