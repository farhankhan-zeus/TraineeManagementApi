using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
namespace TraineeManagementApi.Models{
    
    public enum LearningTaskstatus{
        Draft,
        Published,
        Closed
    }

[Index(nameof(Id),IsUnique =true)]

[Table("LearningTask")]

public class LearningTask
{

   [Key]
    public  Guid Id {get ; set ; }= Guid.NewGuid();
    [Required]  
     [MaxLength(50,ErrorMessage="Title should maximum be 50 characaters")] 
    public required string Title {get ; set ;}
    [Required]
     [MaxLength(500,ErrorMessage="Description should maximum be 500 characaters")]
   
    public required string Descriptiion {get ; set ;}
    
    [Required]
    public required string ExpectedTechStack {get ; set ;}
    [Required]
    public  required string Status {get ; set ;}

    public DateTime DueDate {get ; set ;}
    public DateTime UpdatedDate {get ; set ;}
    public DateTime CreatedDate {get ; set ;}
    
}
}