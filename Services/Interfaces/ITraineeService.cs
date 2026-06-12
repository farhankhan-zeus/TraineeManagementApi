
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;

public interface ITraineeService{
    Task<PagedResponse<TraineeResponseDTO>> GetAll (QuertFilter filter  ,CancellationToken cancellationToken);

    Task<TraineeResponseDTO> GetById ( Guid Id);

    Task<TraineeResponseDTO> AddTrainee( CreateorUpdateTraineeRequestDTO traineedto);

    Task<TraineeResponseDTO> UpdateTrainee(Guid id, CreateorUpdateTraineeRequestDTO updatedTraineedto);

    Task<bool> Delete(Guid Id);

}