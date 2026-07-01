using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.utils;

public static class Contextconfig{
    public static IServiceCollection contextconfiguration(this IServiceCollection services,string connectionstring)
    {
        services.AddDbContext<ApiContext>( options =>{
    options.UseMySQL(connectionstring)
    .UseSeeding((ApiContext, _) =>
    {
        if (ApiContext.Set<User>().Any() == false)
        {
            User newUser= new User{
                Username="admin",
                Email="admin@gmail.com",
                Passwordhash= PasswordHasher.Hashpassword("admin@123"),
                Role= "Admin",
                CreatedDate= DateTime.Now,
                UpdatedDate=DateTime.Now
            };
            ApiContext.Set<User>().Add(newUser);
            ApiContext.SaveChanges();
        }
        ;
    });      
      
});
return services;
    }
}