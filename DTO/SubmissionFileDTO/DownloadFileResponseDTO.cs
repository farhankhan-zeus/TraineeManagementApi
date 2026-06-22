using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.SubmissionFileDTO{
    
    



public class DonwloadFileResponseDTO
{

  

    [Required]
    public required FileStream filestream {get; set;}

    [Required]
    public required string ContentType {get; set;}

    [Required]
    public required string downloadName {get; set;}
   
    
    
}
}