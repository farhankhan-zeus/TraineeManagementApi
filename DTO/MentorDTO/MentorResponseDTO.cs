using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.MentorDTO;
public class MentorResponseDTO
{
     public Guid Id {get ; set ; } 
    [Required]
    
    public  required string FirstName {get ; set ;}
    [Required]
   
    public required string LastName {get ; set ;}
    [Required]
    [EmailAddress]
    public required string Email {get ; set ;}
    [Required]
    public required string Expertise {get ; set ;}
   
    public required string Status {get ; set ;}
}