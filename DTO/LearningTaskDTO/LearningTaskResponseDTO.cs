using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.LearningTaskDTO;
public class LearningTaskResponseDTO
{
     public Guid Id {get ; set ; } 
    [Required]
    
    public  required string Title {get ; set ;}
    [Required]
   
    public required string Descriptiion {get ; set ;}
   
    [Required]
    public required string ExpectedTechStack {get ; set ;}
    [Required]
    public required DateTime DueDate {get; set;}

   [Required]
    public required string Status {get ; set ;}
}