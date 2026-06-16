
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;

public interface ISubmissionService{
    Task<List<SubmissionResponseDTO>> Getall ();

    Task<SubmissionResponseDTO> GetById ( Guid Id);

    Task<SubmissionResponseDTO> AddSubmission( CreateorUpdateSubmissionRequestDTO submission);
    
    Task<SubmissionResponseDTO> UpdateSubmission(Guid id, CreateorUpdateSubmissionRequestDTO updatedsubmission);


}