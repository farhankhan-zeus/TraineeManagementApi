
using TraineeManagementApi.DTO.TraineeDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;

public interface ITraineeService{
    Task<PagedResponse<TraineeResponseDTO>> GetAll (QuertFilter filter  ,CancellationToken cancellationToken);

    Task<TraineeResponseDTO> GetById ( Guid Id,CancellationToken cancellationToken);

    Task<TraineeResponseDTO> AddTrainee( CreateorUpdateTraineeRequestDTO traineedto,CancellationToken cancellationToken);

    Task<TraineeResponseDTO> UpdateTrainee(Guid id, CreateorUpdateTraineeRequestDTO updatedTraineedto,CancellationToken cancellationToken);

    Task<bool> Delete(Guid Id,CancellationToken cancellationToken);

}