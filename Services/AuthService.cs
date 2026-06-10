
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO;
using TraineeManagementApi.DTO.AuthDTO;
using TraineeManagementApi.Models;

using TraineeManagementApi.utils;

namespace TraineeManagementApi.Services{
public class AuthService : IAuthService
{
    private readonly ApiContext _context;
    private readonly IConfiguration _config;
    private readonly int _expiresIn;
    public AuthService(ApiContext context , IConfiguration config)
    {
        _context = context;
        _config = config;
        _expiresIn = Convert.ToInt32(_config["Jwt:expiresIn"]);
    }
    private UserDTO MapToUserDTO(User user)
    {
        return new UserDTO
        {
            Id= user.Id,
            Username=user.Username,
            Email=user.Email,
            Role=user.Role
        };
    }
    
    private string generateJWT(string Username,int Id,int Role )
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    var userClaims = new[]
{
    new Claim(ClaimTypes.NameIdentifier,  Convert.ToString(Id)),
    new Claim(ClaimTypes.Name, Username!),
    new Claim(ClaimTypes.Role, Convert.ToString(Role!))
};

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: userClaims,
        expires: DateTime.Now.AddSeconds(_expiresIn),
        signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
        }
    
    
    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginrequest)
    {
        // string hashedpassword=PasswordHasher.Hashpassword(loginrequest.Password);
        try
        {
            var user = await _context.Users.Where(u=>u.Username== loginrequest.Username ).FirstOrDefaultAsync();
        
            if(user == null) return null;
            // Console.WriteLine(PasswordHasher.VerifyPassword(loginrequest.Password, user.Passwordhash));
            bool isverified =PasswordHasher.VerifyPassword(loginrequest.Password, user.Passwordhash);
            if (isverified)
            {
                UserDTO validuser=MapToUserDTO(user);
                LoginResponseDTO loginResponse = new LoginResponseDTO
                {
                    success=true,
                    token =generateJWT(user.Username,user.Id,Convert.ToInt32(user.Role)),
                    expiresIn=_expiresIn,
                    User=validuser,


                };
                return loginResponse;
            }
                else
                {
                    
            return new LoginResponseDTO
            {
                success=false,      
                User=MapToUserDTO(user)              
            };
                }
           
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
    };

    }
}
}