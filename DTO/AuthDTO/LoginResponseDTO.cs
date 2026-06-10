using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.DTO.AuthDTO;
   

    public class LoginResponseDTO

{
    [Required]
    public required bool success {get ; set ;}


    public  string? token {get ; set ;}

    public int? expiresIn {get ; set ;}

    public UserDTO? User {get ; set ;}




  
};