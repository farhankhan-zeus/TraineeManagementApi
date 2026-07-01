
using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Models;

namespace TraineeManagementApi.DTO.JobProcessing;

public class JobProcessingDTOResponse
{
     public Guid Id {get; set;}

    public required Guid CorrelationId {get; set;}
    
    public int Attempts {get; set;}

    public string? ErrorSummary {get; set;}

    [EnumDataType(typeof(Jobstatus),ErrorMessage="Invalid Status")]
    public required string Status {get; set;}

    public DateTime? Started {get; set;}

    public DateTime? Completed {get; set;}    
}