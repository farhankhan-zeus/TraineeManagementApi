
using TraineeManagementApi.DTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services;

public interface ITraineeService{
    Task<PagedResponse<TraineeResponseDTO>> GetAll (QuertFilter filter  ,CancellationToken cancellationToken);

    Task<TraineeResponseDTO> GetById ( int Id);

    Task<TraineeResponseDTO> AddTrainee(CreateTraineeRequestDTO traineedto);

    Task<TraineeResponseDTO> UpdateTrainee(int id, UpdateTraineeRequestDTO updatedTraineedto);

    Task<bool> Delete(int Id);

}