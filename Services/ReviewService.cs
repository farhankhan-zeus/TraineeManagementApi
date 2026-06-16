

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.ReviewDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.utils;

namespace TraineeManagementApi.Services;

public class ReviewService : IReviewService
{
    private readonly ApiContext _context;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(ApiContext context, ILogger<ReviewService> logger)
    {
        _context = context;
        _logger = logger;
    }

    

     public async Task<List<ReviewResponseDTO>> Getall()
    {
        
            List<Review> reviews = await _context.Reveiws.Include(t=>t.Submission).Include(t=>t.Mentor).ToListAsync();
            if(reviews.Count == 0)
            {
                return [];
            }
            List<ReviewResponseDTO> result =reviews.Select(ResponseDTOMapper.MaptoReviewResponse).ToList();
            return result;
            


        
    }

    public async Task<ReviewResponseDTO> GetById(Guid Id)
    {
       
            Review? review = (Review?) _context.Reveiws.Include(t=>t.Mentor).Include(t=>t.Submission).Where(t=>t.Id==Id);
            if(review == null)
            {
                throw new NotFoundException("Review",Id);
            }
            return ResponseDTOMapper.MaptoReviewResponse(review);
      
    }

    public async Task<ReviewResponseDTO> AddReview(CreateorUpdateReviewRequestDTO review)
       
        
    {
        
        Submission? existSubmission = await _context.Submissions.FindAsync(review.SubmissionId);
        if(existSubmission == null)
        {
            throw new NotFoundException("Submission",review.SubmissionId);
        }
        Mentor? mentor = await _context.Mentors.FindAsync(review.MentorId);
        if(existSubmission == null)
        {
            throw new NotFoundException("Mentor",review.MentorId);
        }
           Review? existreview = _context.Reveiws.FirstOrDefault(r => r.SubmissionId == review.SubmissionId && r.MentorId == review.MentorId);
  if(existreview != null)
            {
                throw new BadRequestException("Review already exist");
            }
            Review newreview = new Review
            {
             
               SubmissionId=review.SubmissionId,
               MentorId=review.MentorId,
               Feedback = review.Feedback,
               ReviewedDate= DateTime.Now,
               ReviewedStatus = review.ReviewedStatus
            };
            if (review.Score != null)
            {
               newreview.Score = review.Score;
            }
            await _context.Reveiws.AddAsync(newreview);
            await _context.SaveChangesAsync();
            return ResponseDTOMapper.MaptoReviewResponse(newreview);
        
        
    }

    public async Task<ReviewResponseDTO> UpdateReview(Guid Id,CreateorUpdateReviewRequestDTO review)
    {
        
            Review? existreview = await _context.Reveiws.FindAsync(Id);
            if(existreview == null)
            {
                throw new NotFoundException("Review",Id);
            }

                
            existreview.SubmissionId=review.SubmissionId;
               existreview.MentorId=review.MentorId;
               existreview.Feedback = review.Feedback;
               existreview.ReviewedDate= DateTime.Now;
               existreview.ReviewedStatus = review.ReviewedStatus;
            if (existreview.Score != null)
            {
               existreview.Score = existreview.Score;
            }
            await _context.SaveChangesAsync();
            return ResponseDTOMapper.MaptoReviewResponse(existreview);

       
    }

  






}