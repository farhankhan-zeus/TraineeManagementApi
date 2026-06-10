
using TraineeManagementApi.DTO;
using TraineeManagementApi.DTO.AuthDTO;

namespace TraineeManagementApi.Services;
public interface IAuthService
{
    Task<LoginResponseDTO> Login (LoginRequestDTO loginrequest);
}