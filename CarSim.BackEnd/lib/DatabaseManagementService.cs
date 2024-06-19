using CarSim.BackEnd.Context;
using Microsoft.EntityFrameworkCore;

namespace CarSim.BackEnd.lib;

public class DatabaseManagementService
{
    public static void MigrateDatabase(IApplicationBuilder app)
    {
        int Try = 0;
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();


        while (!context.Database.CanConnect() && Try <= 5)
        {
            Console.WriteLine("DB not ready...");
            Try += 1;
            Thread.Sleep(5000);
        }

        if (context.Database.CanConnect())
        {
            context.Database.Migrate();
        }

        Try = 0;
    }
}
