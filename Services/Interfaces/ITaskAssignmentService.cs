
using TraineeManagementApi.DTO.TaskAssignmentDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;

public interface ITaskAssignmentService{
    Task<List<TaskAssignmentResponseDTO>> Getall ();

    Task<TaskAssignmentResponseDTO?> GetById ( Guid Id);

    Task<TaskAssignmentResponseDTO> AddTask( CreateorUpdateTaskAssignmentRequestDTO task);

    Task<TaskAssignmentResponseDTO?> UpdateTask(Guid id, CreateorUpdateTaskAssignmentRequestDTO task);

    Task<bool?> DeleteTask(Guid Id);

}