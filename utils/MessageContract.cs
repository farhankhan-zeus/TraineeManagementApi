using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.utils{
    
    



public class SubmissionProcessingRequest
{

   [Key]
    
    public Guid MessageId {get; set;} =Guid.NewGuid();
    
    [Required]
     public Guid CorrelationId {get; set;} =Guid.NewGuid();

    [Required]
     public Guid SubmissionId {get; set;}

    [Required]
    public Guid FileId {get; set;}

    [Required]
    public required SubmissionFile SubmissionFile {get; set;}
   
       public DateTime RequestedAt {get; set;}
    public  string ContractVersion {get; set;} = "v1";
    
    
}
}