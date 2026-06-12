
using TraineeManagementApi.DTO;
using TraineeManagementApi.DTO.AuthDTO;

namespace TraineeManagementApi.Services.Interfaces;
public interface IAuthService
{
    Task<LoginResponseDTO> Login (LoginRequestDTO loginrequest);
}