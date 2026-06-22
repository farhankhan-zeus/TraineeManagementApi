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
    public async Task<IActionResult> Getall(CancellationToken cancellationToken=default)
    {
       
            List<SubmissionResponseDTO> response = await _submissionservice.Getall(cancellationToken);
            _logger.LogInformation("Tasks fetched successfully");
            return Ok( new ApiResponse<List<SubmissionResponseDTO?>>
            {
                success=true,
                message="Data Fetched successfully",
                Data = response
            });
            
        
    }
    [Authorize]
    [HttpGet("{Id:guid}")]    
    public async Task<IActionResult> GetById (Guid Id,CancellationToken cancellationToken=default)
    {
       
        var result = await _submissionservice.GetById(Id,cancellationToken);
       
        _logger.LogInformation($"Submission with Id:{Id} fetched successfully",Id);
        return Ok( new ApiResponse<SubmissionResponseDTO>
            {
                success=true,
                message="Data Fetched successfully",
                Data = result
            });
       

    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddSubmission(  CreateorUpdateSubmissionRequestDTO submission,CancellationToken cancellationToken=default)
    {
        
            SubmissionResponseDTO response = await _submissionservice.AddSubmission(submission,cancellationToken);
            return Ok( new ApiResponse<SubmissionResponseDTO>
            {
                success=true,
                message="TaskAssignment added successfully",
                Data=response
            });
      
    }

    [Authorize]
    [HttpPut("{Id:guid}/status")]
    public async Task<IActionResult> updateSubmission(Guid Id,CreateorUpdateSubmissionRequestDTO updatedsubmission,CancellationToken cancellationToken=default)
    {
        SubmissionResponseDTO response = await _submissionservice.UpdateSubmission(Id, updatedsubmission,cancellationToken);
       
        _logger.LogInformation($"Task Assignment with Id:{Id} updated successfully",Id);
        return Ok(new ApiResponse<SubmissionResponseDTO>
        {
            success=true,
            message="Task successfully",
            Data=response
        });
    
    }
    


}

