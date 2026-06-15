using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.DTO.SubmissionDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class SubmissionController : ControllerBase
{
    private readonly ISubmissionService _submissionservice;
    private readonly ILogger<SubmissionController> _logger;

    public SubmissionController(ISubmissionService submissionService,ILogger<SubmissionController> logger)
    {
        _submissionservice=submissionService;
        _logger=logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Getall()
    {
        try
        {
            List<SubmissionResponseDTO?> response = await _submissionservice.Getall();
            _logger.LogInformation("Tasks fetched successfully");
            return Ok( new ApiResponse<List<SubmissionResponseDTO?>>
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
        var result = await _submissionservice.GetById(Id);
        if(result == null)
        {
            return NotFound( new ApiResponse<object>
            {
                success=true,
                message="Data Fetched successfully",
                Data = {}
            });
        }
        _logger.LogInformation($"TaskAssignment with Id:{Id} fetched successfully",Id);
        return Ok( new ApiResponse<SubmissionResponseDTO>
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
    public async Task<IActionResult> AddSubmission(  CreateorUpdateSubmissionRequestDTO submission)
    {
        try
        {
            SubmissionResponseDTO response = await _submissionservice.AddSubmission(submission);
            return Ok( new ApiResponse<SubmissionResponseDTO>
            {
                success=true,
                message="TaskAssignment added successfully",
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
    public async Task<IActionResult> updateSubmission(Guid Id,CreateorUpdateSubmissionRequestDTO updatedsubmission)
    {
        SubmissionResponseDTO? response = await _submissionservice.UpdateSubmission(Id, updatedsubmission);
        if(response == null)
        {
            return NotFound(new ApiResponse<object>
        {
            success=false,
            message="Task Assignment doesn't exist",
            Data={}
        });
            
        }
        _logger.LogInformation($"Task Assignment with Id:{Id} updated successfully",Id);
        return Ok(new ApiResponse<SubmissionResponseDTO>
        {
            success=true,
            message="Task successfully",
            Data=response
        });
    
    }
    


}

