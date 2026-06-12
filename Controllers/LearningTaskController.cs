using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.DTO.LearningTaskDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class LearningTaskController : ControllerBase
{
    private readonly ILearningTaskService _learningtaskservice;
    private readonly ILogger<LearningTaskController> _logger;

    public LearningTaskController(ILearningTaskService learningTaskService,ILogger<LearningTaskController> logger)
    {
        _learningtaskservice=learningTaskService;
        _logger=logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Getall()
    {
        try
        {
            List<LearningTaskResponseDTO?> response = await _learningtaskservice.Getall();
            _logger.LogInformation("Tasks fetched successfully");
            return Ok( new ApiResponse<List<LearningTaskResponseDTO?>>
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
        var result = await _learningtaskservice.GetById(Id);
        if(result == null)
        {
            return Ok( new ApiResponse<object>
            {
                success=true,
                message="Data Fetched successfully",
                Data = {}
            });
        }
        _logger.LogInformation($"Learning Task with Id:{Id} fetched successfully",Id);
        return Ok( new ApiResponse<LearningTaskResponseDTO>
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
    public async Task<IActionResult> addLearningTask( CreateorUpdateLearningTaskRequestDTO task)
    {
        try
        {
            LearningTaskResponseDTO response = await _learningtaskservice.AddTask(task);
            return Ok( new ApiResponse<LearningTaskResponseDTO>
            {
                success=true,
                message="Mentor added successfully",
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
    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> updateLearningTask (Guid Id,CreateorUpdateLearningTaskRequestDTO task)
    {
        LearningTaskResponseDTO? response = await _learningtaskservice.UpdateTask(Id, task);
        if(response == null)
        {
            return NotFound(new ApiResponse<object>
        {
            success=false,
            message="Mentor doesn't exist",
            Data={}
        });
            
        }
        _logger.LogInformation($"Mentor with Id:{Id} updated successfully",Id);
        return Ok(new ApiResponse<LearningTaskResponseDTO>
        {
            success=true,
            message="Mentor updated successfully",
            Data=response
        });
    
    }
    [Authorize]
    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> deleteLearningTask (Guid Id)
    {
        bool? response = await  _learningtaskservice.DeleteTask(Id);
        if(response == null)
        {
             return NotFound(new ApiResponse<object>
        {
            success=false,
            message="Mentor doesn't exist",
            Data={}
        });
        }
        return NoContent();
    }


}

