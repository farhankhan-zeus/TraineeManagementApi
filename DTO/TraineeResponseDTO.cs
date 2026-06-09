namespace TraineeManagementApi.DTO;
using System.ComponentModel.DataAnnotations;

public class TraineeResponseDTO
{
    public int Id {get ; set ; }
    [Required]
    
    public  required string FirstName {get ; set ;}
    [Required]
   
    public required string LastName {get ; set ;}
    [Required]
    [EmailAddress]
    public required string Email {get ; set ;}
    [Required]
    public required string TechStack {get ; set ;}
    [Required]
    public required string Status {get ; set ;}
    // public DateTime CreatedDate {get ; set ;}
    // public DateTime UpdatedDate {get ; set ;}
    
}