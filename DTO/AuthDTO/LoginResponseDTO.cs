using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.DTO.AuthDTO;
   

    public class LoginResponseDTO

{
    [Required]
    public required bool Success {get ; set ;}


    public  string? Token {get ; set ;}

    public int? expiresIn {get ; set ;}

    public UserDTO? User {get ; set ;}




  
};