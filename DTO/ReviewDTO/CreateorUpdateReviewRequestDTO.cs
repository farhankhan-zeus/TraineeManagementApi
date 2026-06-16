using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.ReviewDTO;
public class CreateorUpdateReviewRequestDTO
{
 
   
    [Required]
   
    public required Guid SubmissionId {get ; set ;}
    [Required]
    public required Guid MentorId {get ; set ;}

   

    [Required]
    public required string Feedback {get; set;}

    [AllowedValues([1,2,3,4,5,6,7,8,9,10,null])]
    public int? Score {get; set;}

    [Required]


 [EnumDataType(typeof(Reviewstatus),ErrorMessage="Invalid Status")]
    public required string ReviewedStatus {get; set;}


}