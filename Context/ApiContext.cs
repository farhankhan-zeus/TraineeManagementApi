using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.Context;

    public class ApiContext : DbContext
    {
         public ApiContext(DbContextOptions<ApiContext> options)
         : base(options){

         }
        public DbSet <Trainee> Trainees {get; set;}
        public DbSet <User> Users {get; set;}
        
    }


