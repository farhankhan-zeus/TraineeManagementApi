using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.SubmissionFileDTO{
    
    



public class SubmissionFileResponseDTO
{

   [Key]
    public  Guid Id {get ; set ; }

    [Required]
    public  required string orignalName {get; set;}
    
    [Required]
    public required string  generatedName {get; set;}

    [Required]
    public required string ContentType {get; set;}

    [Required]
    public required long size {get; set;}

    [Required]
    public required string checksum {get; set;}

    [Required]
    public required Guid UploadedById {get; set;}
    // public TraineeResponseDTO? UploadedBy {get; set;}

    [Required]
    public required Guid SubmissionId {get; set;}
    public SubmissionResponseDTO? Submission {get; set;}

    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    
    
}
}