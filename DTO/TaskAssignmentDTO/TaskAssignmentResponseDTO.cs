using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.DTO.LearningTaskDTO;
using TraineeManagementApi.DTO.MentorDTO;
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.TaskAssignmentDTO;
public class TaskAssignmentResponseDTO
{
  public Guid Id {get; set;}
    public  required Guid TraineeId {get ; set ;}
   public  TraineeResponseDTO? Trainee {get; set;} 
    [Required]
    public required Guid MentorId {get ; set ;}
    public  MentorResponseDTO? Mentor {get; set;} 
    [Required]
    public required Guid LearningTaskId {get ; set ;}
    public  LearningTaskResponseDTO? LearningTask {get; set;}
   
    public  string? Remarks {get ; set ;}
   
    public required  string Status {get ; set ;}

     public DateTime AssignDate {get ; set ;}
        public DateTime DueDate {get ; set ;}
    
}