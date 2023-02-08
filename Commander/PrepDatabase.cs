using Commander.Data;
using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander;

public static class PrepDatabase
{
    public static async Task PrepDbAsync(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<CommanderContext>();
            ArgumentNullException.ThrowIfNull(context);
            if(await CanConnectAsync(context) == false)
            {
                System.Console.WriteLine("Appling Migrations...");
                await SeedDataAsync(context);
            }
            else
            {
                System.Console.WriteLine("No need for Migrations...");
            }
        }
    }

    private static async Task SeedDataAsync(CommanderContext context)
    {
        const int delayInms = 60000;
        const int delayInsec = delayInms/1000;
        const int delayLimit = 5;
        var wait = 0;
        while (wait <= delayLimit)
        {
            System.Console.WriteLine($"Delay {delayInsec} s...");
            await Task.Delay(delayInms);
            wait++;
            if (MigrateDb(context))
            {
                System.Console.WriteLine("Migrated...");
                break;
            }
            if(wait > delayLimit)
            {
                throw new Exception("Cant connect to db...");
            }
        }

        if(context.Commands?.Any() == false)
        {
            System.Console.WriteLine("Adding data - seeding...");
            context.Commands.Add(new Command{ HowTo = "test", Line = "test", Platform = "test"});
            context.SaveChanges();
        }
        else
        {
            System.Console.WriteLine("Already have data - not seeding");
        }
    }

    private static bool MigrateDb(CommanderContext context)
    {
        try
        {
            context.Database.Migrate();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    private static async Task<bool> CanConnectAsync(CommanderContext context)
    {
        try
        {
            _ = await context.Database.ExecuteSqlRawAsync("SELECT 1");
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}