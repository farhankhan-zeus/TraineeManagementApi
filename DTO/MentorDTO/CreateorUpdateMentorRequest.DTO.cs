using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;
namespace TraineeManagementApi.DTO.MentorDTO;
public class CreateorUpdateMentorRequestDTO
{
     [Required(ErrorMessage="First Name is required")]
    [MaxLength(50,ErrorMessage="First Name should maximum be 50 characaters")]
    public required string FirstName {get ; set ;}

    [Required (ErrorMessage="Last Name is required")]
    [MaxLength(50,ErrorMessage="Last Name should maximum be 50 characaters")]    
    public required string LastName {get ; set ;}
    
    [Required(ErrorMessage="Email is required")]
    [EmailAddress]
    public required string Email {get ; set ;}
    [Required]
    public required  string Expertise {get ; set ;}
    [Required]
    [EnumDataType(typeof(Mentorstatus),ErrorMessage="Invalid Status")]
    public required  string Status {get ; set ;}
}