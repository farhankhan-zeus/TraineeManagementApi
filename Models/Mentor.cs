using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
namespace TraineeManagementApi.Models{
    
    public enum Mentorstatus{
        Active,
        Inactive
    }
[Index(nameof(Id),IsUnique =true)]
[Table("Mentors")]
public class Mentor
{

   [Key]
    public  Guid Id {get ; set ; }= Guid.NewGuid();
    [Required]  
     [MaxLength(50,ErrorMessage="First Name should maximum be 50 characaters")] 
    public required string FirstName {get ; set ;}
    [Required]
     [MaxLength(50,ErrorMessage="Last Name should maximum be 50 characaters")]
   
    public required string LastName {get ; set ;}
    [Required]
    [EmailAddress(ErrorMessage ="Valid Email is required")]
    public required string Email {get ; set ;}
    [Required]
    public required string Expertise {get ; set ;}
    [Required]
    public  required string Status {get ; set ;}
    public DateTime CreatedDate {get ; set ;}
    public DateTime UpdatedDate {get ; set ;}
    
}
}