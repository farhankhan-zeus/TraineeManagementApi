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
        
            List<LearningTaskResponseDTO> response = await _learningtaskservice.Getall();
            _logger.LogInformation("Tasks fetched successfully");
            return Ok( new ApiResponse<List<LearningTaskResponseDTO?>>
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
        
        LearningTaskResponseDTO result = await _learningtaskservice.GetById(Id);
        _logger.LogInformation($"Learning Task with Id:{Id} fetched successfully",Id);
        return Ok( new ApiResponse<LearningTaskResponseDTO>
            {
                success=true,
                message="Data Fetched successfully",
                Data = result
            });
        

    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> addLearningTask( CreateorUpdateLearningTaskRequestDTO task)
    {
        
            LearningTaskResponseDTO response = await _learningtaskservice.AddTask(task);
            return Ok( new ApiResponse<LearningTaskResponseDTO>
            {
                success=true,
                message="Mentor added successfully",
                Data=response
            });
       
    }

    [Authorize]
    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> updateLearningTask (Guid Id,CreateorUpdateLearningTaskRequestDTO task)
    {
        LearningTaskResponseDTO response = await _learningtaskservice.UpdateTask(Id, task);
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
        bool response = await  _learningtaskservice.DeleteTask(Id);
        if(response) _logger.LogInformation($"Learning Task with Id: {Id} deleted",Id);
    
        return NoContent();
    }


}

