using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.DTO.TaskAssignmentDTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class TaskAssignmentController : ControllerBase
{
    private readonly ITaskAssignmentService _taskassignmentService;
    private readonly ILogger<TaskAssignmentController> _logger;

    public TaskAssignmentController(ITaskAssignmentService taskAssignmentService,ILogger<TaskAssignmentController> logger)
    {
        _taskassignmentService=taskAssignmentService;
        _logger=logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Getall()
    {
        try
        {
            List<TaskAssignmentResponseDTO?> response = await _taskassignmentService.Getall();
            _logger.LogInformation("Tasks fetched successfully");
            return Ok( new ApiResponse<List<TaskAssignmentResponseDTO?>>
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
        var result = await _taskassignmentService.GetById(Id);
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
        return Ok( new ApiResponse<TaskAssignmentResponseDTO>
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
    public async Task<IActionResult> addTask( CreateorUpdateTaskAssignmentRequestDTO task)
    {
        try
        {
            TaskAssignmentResponseDTO response = await _taskassignmentService.AddTask(task);
            return Ok( new ApiResponse<TaskAssignmentResponseDTO>
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
    public async Task<IActionResult> updateTask(Guid Id,CreateorUpdateTaskAssignmentRequestDTO task)
    {
        TaskAssignmentResponseDTO? response = await _taskassignmentService.UpdateTask(Id, task);
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
        return Ok(new ApiResponse<TaskAssignmentResponseDTO>
        {
            success=true,
            message="Task successfully",
            Data=response
        });
    
    }
    [Authorize]
    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> deleteTask (Guid Id)
    {
        bool? response = await  _taskassignmentService.DeleteTask(Id);
        if(response == null)
        {
             return NotFound(new ApiResponse<object>
        {
            success=false,
            message="Task doesn't exist",
            Data={}
        });
        }
        return NoContent();
    }


}

