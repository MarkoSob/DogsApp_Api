using DogsApp_DAL;
using Microsoft.EntityFrameworkCore;

namespace DogsApp_Api
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication? webApp)
        {
            using (var scope = webApp!.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<EfDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return webApp;
        }
    }
}
