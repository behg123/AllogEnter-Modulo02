using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;

namespace Univali.Api.Extensions;

internal static class StartupHelperExtensions
{
    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            try
            {
                var customerContext = scope.ServiceProvider.GetService<CustomerContext>();
                if (customerContext != null)
                {
                    await customerContext.Database.EnsureDeletedAsync();
                    await customerContext.Database.MigrateAsync();
                }
                
                var PublisherContext = scope.ServiceProvider.GetService<PublisherContext>();
                if (PublisherContext != null)
                {
                    await PublisherContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                // var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                // logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}
