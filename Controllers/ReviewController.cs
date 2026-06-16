using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.DTO.ReviewDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewservice;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IReviewService reviewService,ILogger<ReviewController> logger)
    {
        _reviewservice=reviewService;
        _logger=logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Getall()
    {
       
            List<ReviewResponseDTO> response = await _reviewservice.Getall();
            _logger.LogInformation("Tasks fetched successfully");
            return Ok( new ApiResponse<List<ReviewResponseDTO?>>
            {
                success=true,
                message="Data Fetched successfully",
                Data = response
            });
            
       
    }
    [Authorize]
    [HttpGet("{Id:guid}")]    
    public async Task<IActionResult> GetById (Guid Id)
    {
      
        var result = await _reviewservice.GetById(Id);
        
        _logger.LogInformation($"Review with Id:{Id} fetched successfully",Id);
        return Ok( new ApiResponse<ReviewResponseDTO>
            {
                success=true,
                message="Data Fetched successfully",
                Data = result
            });
       

    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddReview(  CreateorUpdateReviewRequestDTO submission)
    {
        
            ReviewResponseDTO response = await _reviewservice.AddReview(submission);
            return Ok( new ApiResponse<ReviewResponseDTO>
            {
                success=true,
                message="Review added successfully",
                Data=response
            });
       
    }

    [Authorize]
    [HttpPut("{Id:guid}/status")]
    public async Task<IActionResult> updateReview(Guid Id,CreateorUpdateReviewRequestDTO updatedsubmission)
    {
        ReviewResponseDTO response = await _reviewservice.UpdateReview(Id, updatedsubmission);
       
        _logger.LogInformation($"Review with Id:{Id} updated successfully",Id);
        return Ok(new ApiResponse<ReviewResponseDTO>
        {
            success=true,
            message="Review Updated successfully",
            Data=response
        });
    
    }
    


}

