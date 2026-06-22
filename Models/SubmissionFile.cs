using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace TraineeManagementApi.Models{
    
    

[Index(nameof(Id),IsUnique =true)]

[Table("SubmissionFile")]

public class SubmissionFile
{

   [Key]
    public  Guid Id {get ; set ; }= Guid.NewGuid();

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
    public  Guid UploadedById {get; set;}
    // public User? UploadedBy {get; set;}=null;

    [Required]
    public  Guid SubmissionId {get; set;}
    public Submission? Submission {get; set;}=null;

    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    
    
}
}