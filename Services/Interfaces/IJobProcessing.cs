
using TraineeManagementApi.DTO.JobProcessing;

namespace TraineeManagementApi.Services.Interfaces;

public interface IJobProcessing
{
    public Task<JobProcessingDTOResponse> getJob(Guid Id,CancellationToken cancellationToken);
}