using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace TraineeManagementApi.Models{
    
    public enum Submissionstatus{
        Submitted,
        Resubmitted
    }

[Index(nameof(Id),IsUnique =true)]

[Table("Submission")]

public class Submission
{

   [Key]
    public  Guid Id {get ; set ; }= Guid.NewGuid();

    [Required]
    public  Guid TaskAssignmentId {get; set;}
    public TaskAssignment? TaskAssignment {get; set;} = null;
    
    [RegularExpression(@"^(https?:\/\/)(www\.)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(:\d+)?(\/[^\s]*)?$
",ErrorMessage ="Invalid Url")]
    public  required string SubmissionUrl {get ; set ;}

    public string? Notes {get; set;}

    [Required]
    public required DateTime SubmittedDate {get ; set ;}

    public required string Status {get; set;}
    
    
}
}