using CarSim.BackEnd.Context;
using Microsoft.EntityFrameworkCore;

namespace CarSim.BackEnd.Services
{
    public class DatabaseManagementService
    {
        
        public static void MigrateDatabase(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();


            while (!context.Database.CanConnect())
            {
                Console.WriteLine("DB not ready...");
                Thread.Sleep(5000);
            }

            context.Database.Migrate();
        }
    }
}
