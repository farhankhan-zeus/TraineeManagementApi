using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
namespace TraineeManagementApi.Models{
    
    public enum Traineestatus{
        Active,
        Inactive
    }

[Table("Trainees")]
public class Trainee
{

   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public  int Id {get ; set ; }
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
    public required string TechStack {get ; set ;}
    [Required]
    public  required string Status {get ; set ;}
    public DateTime CreatedDate {get ; set ;}
    public DateTime UpdatedDate {get ; set ;}
    
}
}