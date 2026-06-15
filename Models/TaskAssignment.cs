using TraineeManagementApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace TraineeManagementApi.Models;

public enum TaskAssignmentstatus
{
    Assigned,
    Inprogress,
     Submitted,
     Reviewed,
     Completed
}

[Index(nameof(Id),IsUnique =true)]

[Table("TaskAssignment")]
public class TaskAssignment : IValidatableObject
{
    [Key]
    public Guid Id {get; set;} =  Guid.NewGuid();

    [Required]
    public Guid TraineeId {get; set;}
    // [Required]
    [Required]
    public Guid MentorId {get; set;}
      public Guid LearningTaskId {get; set;}
 public Trainee Trainee { get; set; } = null!;
    public Mentor Mentor { get; set; } = null!;
    public LearningTask LearningTask { get; set; } = null!;
        // [Required]
      [Required]
      public required DateTime AssignDate {get; set;}
      [Required]
      
      public required DateTime DueDate {get; set;}
      [Required]
      public required string Status {get; set;}

        [Required]
        public string? Remarks {get; set;}

         public DateTime UpdatedDate {get ; set ;}
        public DateTime CreatedDate {get ; set ;}
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