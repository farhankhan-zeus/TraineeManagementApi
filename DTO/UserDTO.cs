using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.DTO;
   

    public class UserDTO

{
    [Required]
    public required int Id {get ; set ;}


    [Required(ErrorMessage ="Username is required")]
    public required string Username {get ; set ;}

    [Required(ErrorMessage ="Email is required")]
    [EmailAddress]
    public required string Email {get ; set ;}

    [EnumDataType(typeof(RoleType),ErrorMessage ="Invalid Role")]
    public required RoleType Role {get ; set ;}
 

};