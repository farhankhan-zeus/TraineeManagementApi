
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;

public interface ISubmissionService{
    Task<List<SubmissionResponseDTO>> Getall (CancellationToken cancellationToken);

    Task<SubmissionResponseDTO> GetById ( Guid Id,CancellationToken cancellationToken);

    Task<SubmissionResponseDTO> AddSubmission( CreateorUpdateSubmissionRequestDTO submission, CancellationToken cancellationToken);
    
    Task<SubmissionResponseDTO> UpdateSubmission(Guid id, CreateorUpdateSubmissionRequestDTO updatedsubmission, CancellationToken cancellationToken);


}