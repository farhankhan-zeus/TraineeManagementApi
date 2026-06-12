using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.LearningTaskDTO;
public class CreateorUpdateLearningTaskRequestDTO
{
    [Required]  
     [MaxLength(50,ErrorMessage="Title should maximum be 50 characaters")] 
    public required string Title {get ; set ;}
    [Required]
     [MaxLength(500,ErrorMessage="Description should maximum be 500 characaters")]
   
    public required string Descriptiion {get ; set ;}
    
   
    [Required]
    public required  string ExpectedTechStack {get ; set ;}
    [Required]
    [EnumDataType(typeof(LearningTaskstatus),ErrorMessage="Invalid Status")]
    public required  string Status {get ; set ;}

       public DateTime DueDate {get ; set ;}
}