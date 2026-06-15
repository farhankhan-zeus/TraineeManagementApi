
using TraineeManagementApi.DTO.ReviewDTO;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;

public interface IReviewService{
    Task<List<ReviewResponseDTO>> Getall ();

    Task<ReviewResponseDTO?> GetById ( Guid Id);

    Task<ReviewResponseDTO> AddReview( CreateorUpdateReviewRequestDTO review);
    
    Task<ReviewResponseDTO?> UpdateReview(Guid id, CreateorUpdateReviewRequestDTO updatedReview);


}