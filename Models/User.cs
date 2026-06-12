using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace TraineeManagementApi.Models;
    public  enum RoleType{
        
        Admin,
        Trainee,
        Mentor
    }

[Table("Users")]
[Index(nameof(Username),IsUnique =true)]
public class User
{
    [Key]
     public  Guid Id {get ; set ; }= Guid.NewGuid();
    [Required]   
     [MaxLength(50,ErrorMessage="Username should maximum be 50 characaters")]
     [MinLength(5,ErrorMessage="Username should minimum be 5 characaters")]
    public required string Username {get ; set ;}
    [Required]
   

    [EmailAddress(ErrorMessage ="Valid Email is required")]
    public required string Email {get ; set ;}
    [Required]
    public required string Passwordhash {get ; set ;}
    [Required]
    public required string Role{get ; set ;}
    public DateTime CreatedDate {get ; set ;}
    public DateTime UpdatedDate {get ; set ;}
    
}