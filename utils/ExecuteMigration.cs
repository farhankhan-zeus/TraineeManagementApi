using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;

namespace TraineeManagementApi.utils;

public static class DbMigrations{
    public static async Task MigrateDatabse (this WebApplication app)
    {
        try{
        using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApiContext>();
    Console.WriteLine("Starting Migration");
     
    dbContext.Database.Migrate(); 
}
    }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to Migrate Data {e.Message}",e);
            throw e;
        }
    }
}