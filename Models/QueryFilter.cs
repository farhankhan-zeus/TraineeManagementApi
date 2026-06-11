namespace TraineeManagementApi.Models;

public class QuertFilter
{
    public int PageNumber {get; set;} =1;
    public int PageSize {get; set;} =10;
    public string? Search {get; set;}

    public string? Status {get; set;}
}