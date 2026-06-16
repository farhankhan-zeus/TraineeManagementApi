using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.DTO.MentorDTO;
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.ReviewDTO;
public class ReviewResponseDTO
{
    public Guid Id {get; set;}
   
    [Required]
   
    public required Guid SubmissionId {get ; set ;}

    public SubmissionResponseDTO? Submission {get; set;}
    [Required]
    public required Guid MentorId {get ; set ;}

    public MentorResponseDTO? Mentor {get; set;}

   

    [Required]
    public required string Feedback {get; set;}

    [AllowedValues([1,2,3,4,5,6,7,8,9,10])]
    public int? Score {get; set;}

    [Required]
    public required DateTime ReviewedDate {get ; set ;}

    public required string ReviewedStatus {get; set;}


}