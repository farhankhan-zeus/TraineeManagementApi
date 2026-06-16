using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace TraineeManagementApi.Models{
    
    public enum Reviewstatus{
        Accepted,
        Changes_Required,
        Rejected
    }

[Index(nameof(Id),IsUnique =true)]

[Table("Review")]

public class Review
{

   [Key]
    public  Guid Id {get ; set ; }= Guid.NewGuid();

    [Required]
    public  Guid SubmissionId {get; set;}
    public  Submission? Submission {get; set;} = null;
    public  Guid MentorId {get; set;}
    public Mentor? Mentor {get; set;}= null;
    
    [Required]
    public required string Feedback {get; set;}

    [AllowedValues([1,2,3,4,5,6,7,8,9,10,null])]
    public int? Score {get; set;}

    [Required]
    public required DateTime ReviewedDate {get ; set ;}

    public required string ReviewedStatus {get; set;}
    
    
}
}