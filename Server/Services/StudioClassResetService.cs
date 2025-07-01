using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using System.Linq;

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

            bool isSaturdayNight = now.DayOfWeek == DayOfWeek.Saturday && now.Hour >= 20;
            bool isSunday = now.DayOfWeek == DayOfWeek.Sunday;

            if ((isSaturdayNight || isSunday) && !_hasRunToday)
            {
                await ResetClassesAsync();
                _hasRunToday = true;
            }
            else if (now.DayOfWeek != DayOfWeek.Saturday && now.DayOfWeek != DayOfWeek.Sunday)
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

            if (!allClasses.Any())
            {
                Console.WriteLine("⚠️ No studio classes found to reset.");
                return;
            }

            DateTime today = DateTime.Today;
            DateTime thisSunday = today.AddDays(-((int)today.DayOfWeek)).Date;
            if (today.DayOfWeek == DayOfWeek.Sunday)
                thisSunday = today;

            DateTime firstLessonDate = allClasses.Min(c => c.Date.Date);
            if (firstLessonDate >= thisSunday)
            {
                Console.WriteLine("✅ Studio classes already aligned to this week.");
                return;
            }

            int deltaDays = (thisSunday - firstLessonDate).Days;
            foreach (var c in allClasses)
            {
                c.Date = c.Date.AddDays(deltaDays);
                c.CurrentNum = 20;
                c.IsCancelled = false;
            }
            db.GymnastClasses.RemoveRange(db.GymnastClasses);

            var allGymnasts = await db.Gymnasts.ToListAsync();
            foreach (var gymnast in allGymnasts)
            {
                switch (gymnast.MemberShipType)
                {
                    case nameof(MembershipTypeEnum.Monthly_Standard):
                    case nameof(MembershipTypeEnum.Yearly_Standard):
                        gymnast.WeeklyCounter = 2;
                        break;
                    case nameof(MembershipTypeEnum.Monthly_Pro):
                    case nameof(MembershipTypeEnum.Yearly_Pro):
                        gymnast.WeeklyCounter = 9999;
                        break;
                }
            }

            await db.SaveChangesAsync();
            Console.WriteLine("✅ Studio classes reset, gymnast weekly counters refreshed, and aligned to this Sunday.");
        }
    }
}
