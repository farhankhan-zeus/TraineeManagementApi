using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraineeManagementApi.DTO.JobProcessing;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Controllers;

[ApiController]

public class JobProcessingController: ControllerBase
{
    private readonly IJobProcessing _jobprocessing;
    private readonly ILogger<JobProcessingController> _logger;
   
    public JobProcessingController(IJobProcessing jobProcessing,ILogger<JobProcessingController> logger)
    {
        _jobprocessing=jobProcessing;
        _logger=logger;
    }
    
    [Authorize]
    [HttpGet("api/ProcessingJobs/{Id}")]
    public async Task<IActionResult> getJob([FromRoute] Guid Id,CancellationToken cancellationToken=default)
    {
        
        if (!ModelState.IsValid)
            {
                throw new BadRequestException("Invalid Data");
            }
        JobProcessingDTOResponse response = await _jobprocessing.getJob(Id,cancellationToken);
        return Ok(new ApiResponse<JobProcessingDTOResponse>
        {
            success=true,
            message="Job Status fetched",
            Data=response
        });
       



    }
}