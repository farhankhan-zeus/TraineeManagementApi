using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.TaskAssignmentDTO;
public class TaskAssignmentResponseDTO
{
    public Guid Id {get; set;}
    public  required Guid TraineeId {get ; set ;}
    [Required]
   
    public required Guid MentorId {get ; set ;}
    [Required]
    
    public required Guid LearningTaskId {get ; set ;}
   
    public  string? Remarks {get ; set ;}
   
    public required  string Status {get ; set ;}

     public DateTime AssignDate {get ; set ;}
        public DateTime DueDate {get ; set ;}
    
}