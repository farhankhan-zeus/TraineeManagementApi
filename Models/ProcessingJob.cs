
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TraineeManagementApi.Models;

public enum Jobstatus
{
    Queued,
    Processing,
    Completed,
    Failed
}

[Index(nameof(Id),IsUnique =true)]
[Table("ProcessingJob")]
public class ProcessingJob
{
    [Key]
    public Guid Id {get; set;} = new Guid();

    public required Guid CorrelationId {get; set;}
    
    public int Attempts {get; set;}= 0;

    public string? ErrorSummary {get; set;}

    [EnumDataType(typeof(Jobstatus),ErrorMessage="Invalid Status")]
    public required string Status {get; set;}

    public DateTime? Started {get; set;}

    public DateTime? Completed {get; set;}    

}