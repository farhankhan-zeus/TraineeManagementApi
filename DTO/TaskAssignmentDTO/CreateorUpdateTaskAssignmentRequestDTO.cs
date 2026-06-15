using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.TaskAssignmentDTO;
public class CreateorUpdateTaskAssignmentRequestDTO:IValidatableObject
{
 
    public  required Guid TraineeId {get ; set ;}
    [Required]
   
    public required Guid MentorId {get ; set ;}
    [Required]
    
    public required Guid LearningTaskId {get ; set ;}
   
    public  string? Remarks {get ; set ;}
   
    [EnumDataType(typeof(TaskAssignmentstatus),ErrorMessage="Invalid Status")]
    public required  string Status {get ; set ;}

     public DateTime AssignDate {get ; set ;}
        public DateTime DueDate {get ; set ;}
     public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
        if (DueDate < AssignDate)
            {
                yield return new ValidationResult(
                errorMessage: "DueDate should always be greater than AssignDate."
                
                );
            }
        }
}