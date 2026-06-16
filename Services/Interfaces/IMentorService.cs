
using TraineeManagementApi.DTO.MentorDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;

public interface IMentorService{
    Task<List<MentorResponseDTO>> Getall ();

    Task<MentorResponseDTO> GetById ( Guid Id);

    Task<MentorResponseDTO> AddMentor( CreateorUpdateMentorRequestDTO mentee);

    Task<MentorResponseDTO> UpdateMentor(Guid id, CreateorUpdateMentorRequestDTO updatedmentee);

    Task<bool> DeleteMentor(Guid Id);

}