using TraineeManagementApi.Services;
using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Models;
using TraineeManagementApi.Context;
using TraineeManagementApi.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String not found");

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApiContext>( options =>{
    options.UseMySQL(connectionString)
    .UseSeeding((ApiContext, _) =>
    {
        if (ApiContext.Set<User>().Any() == false)
        {
            User newUser= new User{
                Username="admin",
                Email="admin@gmail.com",
                Passwordhash= PasswordHasher.Hashpassword("admin@123"),
                Role= RoleType.Admin,
                CreatedDate= DateTime.Now,
                UpdatedDate=DateTime.Now



            };
            ApiContext.Set<User>().Add(newUser);
            ApiContext.SaveChanges();
        }
        ;
    });        
});
builder.Services.AddScoped<ITraineeService,TraineeService>();
builder.Services.AddScoped<IAuthService,AuthService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
            };
        });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
