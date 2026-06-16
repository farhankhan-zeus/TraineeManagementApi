using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.DTO.AuthDTO;
namespace TraineeManagementApi.Controllers;



[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login( LoginRequestDTO loginrequest)
    {
        
            LoginResponseDTO? response =await _authService.Login(loginrequest);
            if (response== null)
            {
                return NotFound();
            }
            if (response.Success == false)
            {
                return BadRequest(new ApiResponse<Object>{success=false,message="Wrong Password",Data={}});
            }
            return Ok( new ApiResponse<LoginResponseDTO>
            {
                success=true,
                message="Login successful",
                Data=response
            });
       
    }



}