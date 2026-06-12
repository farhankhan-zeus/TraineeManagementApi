
using TraineeManagementApi.DTO.LearningTaskDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;

public interface ILearningTaskService{
    Task<List<LearningTaskResponseDTO>> Getall ();

    Task<LearningTaskResponseDTO?> GetById ( Guid Id);

    Task<LearningTaskResponseDTO> AddTask( CreateorUpdateLearningTaskRequestDTO mentee);

    Task<LearningTaskResponseDTO?> UpdateTask(Guid id, CreateorUpdateLearningTaskRequestDTO updatedmentee);

    Task<bool?> DeleteTask(Guid Id);

}