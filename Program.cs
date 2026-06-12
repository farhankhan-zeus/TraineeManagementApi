using TraineeManagementApi.Services;
using TraineeManagementApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Models;
using TraineeManagementApi.Context;
using TraineeManagementApi.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins ="_myAllowedSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://localhost:5173")
                                .AllowCredentials();
                      });
});

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String not found");
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
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddScoped<ITraineeService,TraineeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMentorService,MentorService>();
builder.Services.AddScoped<ILearningTaskService,LearningTaskService>();

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

builder.Services.AddAuthorization();

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

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
