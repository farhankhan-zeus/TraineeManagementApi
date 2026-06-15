using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.SubmissionDTO;
public class CreateorUpdateSubmissionRequestDTO
{
 
   
    [Required]
   
    public required Guid TaskAssignmentId {get ; set ;}

    [Required]    
   
    public  required string SubmissionUrl {get ; set ;}

    public string? Notes {get; set;}

    


}