using CarSim.BackEnd.Context;
using Microsoft.EntityFrameworkCore;

namespace CarSim.BackEnd.Services
{
    public class DatabaseManagementService
    {
        private ILogger _log;

        public DatabaseManagementService(ILogger logger)
        {
            _log = logger;
        }
        public static void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {

                serviceScope.ServiceProvider.GetService<DataContext>().Database.Migrate();
            }
        }
    }
}
