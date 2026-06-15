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
        try
        {
            List<ReviewResponseDTO?> response = await _reviewservice.Getall();
            _logger.LogInformation("Tasks fetched successfully");
            return Ok( new ApiResponse<List<ReviewResponseDTO?>>
            {
                success=true,
                message="Data Fetched successfully",
                Data = response
            });
            
        }
        catch (Exception)
        {
            return StatusCode(500,new ApiResponse<object>
            {
                success=false,
                message="Internal Server Error",
                Data={}
            });
        }
    }
    [Authorize]
    [HttpGet("{Id:guid}")]    
    public async Task<IActionResult> GetById (Guid Id)
    {
        try{
        var result = await _reviewservice.GetById(Id);
        if(result == null)
        {
            return NotFound( new ApiResponse<object>
            {
                success=true,
                message="Data Fetched successfully",
                Data = {}
            });
        }
        _logger.LogInformation($"Review with Id:{Id} fetched successfully",Id);
        return Ok( new ApiResponse<ReviewResponseDTO>
            {
                success=true,
                message="Data Fetched successfully",
                Data = result
            });
        }
        catch( Exception )
        {
            return StatusCode(500,new ApiResponse<object>
            {
                success=false,
                message="Internal Server Error",
                Data={}
            });
        }

    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddReview(  CreateorUpdateReviewRequestDTO submission)
    {
        try
        {
            ReviewResponseDTO response = await _reviewservice.AddReview(submission);
            return Ok( new ApiResponse<ReviewResponseDTO>
            {
                success=true,
                message="Review added successfully",
                Data=response
            });
        }
        catch (Exception)
        {
            
            return StatusCode(500,new ApiResponse<object>
            {
                success=false,
                message="Internal Server Error",
                Data={}
            });
        }
    }

    [Authorize]
    [HttpPut("{Id:guid}/status")]
    public async Task<IActionResult> updateReview(Guid Id,CreateorUpdateReviewRequestDTO updatedsubmission)
    {
        ReviewResponseDTO? response = await _reviewservice.UpdateReview(Id, updatedsubmission);
        if(response == null)
        {
            return NotFound(new ApiResponse<object>
        {
            success=false,
            message="Review doesn't exist",
            Data={}
        });
            
        }
        _logger.LogInformation($"Review with Id:{Id} updated successfully",Id);
        return Ok(new ApiResponse<ReviewResponseDTO>
        {
            success=true,
            message="Review Updated successfully",
            Data=response
        });
    
    }
    


}

