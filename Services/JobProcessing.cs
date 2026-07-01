using TraineeManagementApi.Context;
using TraineeManagementApi.DTO.JobProcessing;
using TraineeManagementApi.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.utils;

namespace TraineeManagementApi.Services;

public class JobProcessing : IJobProcessing
{
    private readonly ILogger<JobProcessing> _logger;
    private readonly ApiContext _context;

    public JobProcessing(ApiContext context,ILogger<JobProcessing> logger)
    {
        _context=context;
        _logger=logger;
    }

    public async Task<JobProcessingDTOResponse> getJob (Guid Id,CancellationToken cancellationToken)
    {
          if (!Guid.TryParse(Convert.ToString(Id), out Guid validId))
        {
            throw new BadRequestException("Invalid Id");
        }
        ProcessingJob? job = await _context.ProcessingJobs.FindAsync(Id);
        if(job is null)
        {
            _logger.LogWarning($"Job with Id: {Id} not found");
            throw new NotFoundException("Job",Id);
        }
        return ResponseDTOMapper.MapJobProcessing(job);
        

    }
}