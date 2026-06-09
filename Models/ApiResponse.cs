
using System.ComponentModel.DataAnnotations;
namespace TraineeManagementApi.Models;

public class ApiResponse<T>{
    public bool success {set; get;}
    public string? message {set; get;}
    public T? Data {set; get;}

}