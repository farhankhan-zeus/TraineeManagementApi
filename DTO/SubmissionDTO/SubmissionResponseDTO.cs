using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.SubmissionDTO;
public class SubmissionResponseDTO
{
 
   public  Guid Id {get ; set ; }
    [Required]
   
    public required Guid TaskAssignmentId {get ; set ;}

    [Required]    
   [RegularExpression(@"^(https?:\/\/)(www\.)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(:\d+)?(\/[^\s]*)?$
",ErrorMessage ="Invalid Url")]
    public  required string SubmissionUrl {get ; set ;}

    public string? Notes {get; set;}

    [Required]
    public required DateTime SubmittedDate {get ; set ;}

    public required string Status {get; set;}
}