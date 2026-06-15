

using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.ReviewDTO;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

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

    public ReviewResponseDTO MaptoReviewResponse( Review review)
    {
        ReviewResponseDTO response=new ReviewResponseDTO{
        Id = review.Id,
        SubmissionId = review.SubmissionId,
        MentorId = review.MentorId,
        Feedback = review.Feedback,
        ReviewedDate = review.ReviewedDate,
        ReviewedStatus=review.ReviewedStatus,

    };
        if(review.Score != null)
        {
            response.Score = review.Score;
        }
        return response;
    }

     public async Task<List<ReviewResponseDTO?>> Getall()
    {
        try
        {
            List<Review>? reviews = await _context.Reveiws.ToListAsync();
            if(reviews.Count == 0)
            {
                return [];
            }
            List<ReviewResponseDTO?> result =reviews.Select(MaptoReviewResponse).ToList();
            return result;
            


        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to get the Reviews");
            throw new Exception("Error getting the Reviews",e);
        }
    }

    public async Task<ReviewResponseDTO?> GetById(Guid Id)
    {
        try
        {
            Review? review = await _context.Reveiws.FindAsync(Id);
            if(review == null)
            {
                throw new NotFoundException("Review",Id);
            }
            return MaptoReviewResponse(review);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"failed to retrive review with Id: {Id}",Id);
            throw new Exception("Failed to Retrive review",e);
        }
    }

    public async Task<ReviewResponseDTO> AddReview(CreateorUpdateReviewRequestDTO review)
       
        
    {
        try
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
            return MaptoReviewResponse(newreview);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Failed to add review");
            throw new Exception("Failed to add review",e);
        }
        
    }

    public async Task<ReviewResponseDTO?> UpdateReview(Guid Id,CreateorUpdateReviewRequestDTO review)
    {
        try
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
            return MaptoReviewResponse(existreview);

        }
        catch (Exception e)
        {
            _logger.LogWarning($"Failed to update review with Id: {Id}",Id);
            
            throw new Exception("Failed to update review",e);
        }
    }

  






}