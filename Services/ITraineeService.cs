
using TraineeManagementApi.DTO;

namespace TraineeManagementApi.Services;

public interface ITraineeService{
    Task<List<TraineeResponseDTO>> GetAll (string search);

    Task<TraineeResponseDTO> GetById ( int Id);

    Task<TraineeResponseDTO> AddTrainee(CreateTraineeRequestDTO traineedto);

    Task<TraineeResponseDTO> UpdateTrainee(int id, UpdateTraineeRequestDTO updatedTraineedto);

    Task<bool> Delete(int Id);

}