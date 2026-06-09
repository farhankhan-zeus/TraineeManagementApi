using System.ComponentModel.DataAnnotations;
namespace TraineeManagementApi.DTO
{
    public enum Traineestatus{
        Active,
        Inactive
    }


    public class UpdateTraineeRequestDTO
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
    public required string TechStack {get ; set ;}
    [Required]
    [EnumDataType(typeof(Traineestatus),ErrorMessage="Invalid Status")]
    public required string Status {get ; set ;}
    }
}
